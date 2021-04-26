using IniParser;
using Mutagen.Bethesda.Archives.Ba2;
using Mutagen.Bethesda.Archives.Bsa;
using Mutagen.Bethesda.Inis;
using Mutagen.Bethesda.Plugins;
using Noggog;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Mutagen.Bethesda.Archives
{
    /// <summary>
    /// A readonly interface for retrieving data from a Bethesda Archive
    /// </summary>
    public interface IArchiveReader
    {
        /// <summary>
        /// Attempts to locate and retrieve a folder from the archive
        /// </summary>
        /// <param name="path">Folder path to look for</param>
        /// <param name="folder">Retrieved folder object, if successful</param>
        /// <returns>True if folder with the given path was located in the archive</returns>
        bool TryGetFolder(string path, [MaybeNullWhen(false)] out IArchiveFolder folder);
        IEnumerable<IArchiveFile> Files { get; }
    }

    public static class Archive
    {
        // ToDo
        // Migrate this to a more centralized/official place within Mutagen
        internal static IFileSystem FileSystem = new FileSystem();

        /// <summary>
        /// Returns the preferred extension (.bsa/.ba2) depending on the Game Release
        /// </summary>
        /// <param name="release"></param>
        /// <returns>Archive extension used by the given game release, with period delimiter.</returns>
        public static string GetExtension(GameRelease release)
        {
            switch (release.ToCategory())
            {
                case GameCategory.Oblivion:
                case GameCategory.Skyrim:
                    return ".bsa";
                case GameCategory.Fallout4:
                    return ".ba2";
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates an Archive reader object from the given path, for the given Game Release.
        /// </summary>
        /// <param name="release">GameRelease the archive is for</param>
        /// <param name="path">Path to create archive reader from</param>
        /// <returns>Archive reader object</returns>
        public static IArchiveReader CreateReader(GameRelease release, string path)
        {
            switch (release)
            {
                case GameRelease.Oblivion:
                case GameRelease.SkyrimLE:
                case GameRelease.SkyrimSE:
                case GameRelease.SkyrimVR:
                    return new BsaReader(path);
                case GameRelease.Fallout4:
                    return new Ba2Reader(path);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Enumerates all applicable Archives for a given release and ModKey that are within a given dataFolderPath.
        /// Orders the results to an Archive load order driven by the ini
        /// </summary>
        /// <param name="release">GameRelease to query for</param>
        /// <param name="dataFolderPath">Folder to query within</param>
        /// <param name="modKey">ModKey to query about</param>
        /// <returns></returns>
        public static IEnumerable<string> GetApplicableArchivePaths(GameRelease release, string dataFolderPath, ModKey modKey)
        {
            return GetApplicableArchivePaths(release, dataFolderPath, modKey, GetPriorityOrderComparer(release));
        }

        /// <summary>
        /// Enumerates all applicable Archives for a given release and ModKey that are within a given dataFolderPath.
        /// </summary>
        /// <param name="release">GameRelease to query for</param>
        /// <param name="dataFolderPath">Folder to query within</param>
        /// <param name="modKey">ModKey to query about</param>
        /// <param name="archiveOrdering">Archive ordering overload.  Empty enumerable means no ordering.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetApplicableArchivePaths(GameRelease release, string dataFolderPath, ModKey modKey, IEnumerable<string> archiveOrdering)
        {
            return GetApplicableArchivePaths(release, dataFolderPath, modKey, GetPriorityOrderComparer(release, archiveOrdering));
        }

        /// <summary>
        /// Enumerates all applicable Archives for a given release and ModKey that are within a given dataFolderPath.
        /// Orders the results to an Archive load order driven by the ini unless specified otherwise
        /// </summary>
        /// <param name="release">GameRelease to query for</param>
        /// <param name="dataFolderPath">Folder to query within</param>
        /// <param name="modKey">ModKey to query about</param>
        /// <param name="archiveOrdering">How to order the archive paths.  Null for no ordering</param>
        /// <returns>Full paths of Archives that apply to the given mod and exist</returns>
        public static IEnumerable<string> GetApplicableArchivePaths(GameRelease release, string dataFolderPath, ModKey modKey, IComparer<string>? archiveOrdering = null)
        {
            var iniListedArchives = GetIniListings(release).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var ret = Directory.EnumerateFiles(dataFolderPath, $"*{GetExtension(release)}")
                .Where(archive =>
                {
                    var archiveFileName = Path.GetFileName(archive);
                    if (archiveFileName == null) return false;
                    if (iniListedArchives.Contains(archiveFileName)) return true;
                    return IsApplicable(release, modKey, archiveFileName);
                });
            if (archiveOrdering != null)
            {
                return ret.OrderBy(x => x, archiveOrdering);
            }
            return ret;
        }

        /// <summary>
        /// Analyzes whether an Archive would typically apply to a given ModKey. <br />
        ///  <br />
        /// - Is extension of the proper type <br />
        /// - Does the name match <br />
        /// - Does the name match, with an extra ` - AssetType` suffix considered
        /// </summary>
        /// <param name="release">Game Release of mod</param>
        /// <param name="modKey">ModKey to check applicability for</param>
        /// <param name="archiveFileName">Filename of the Archive, with extension</param>
        /// <returns>True if Archive is typically applicable to the given ModKey</returns>
        public static bool IsApplicable(GameRelease release, ModKey modKey, ReadOnlySpan<char> archiveFileName)
        {
            if (!Path.GetExtension(archiveFileName).Equals(GetExtension(release), StringComparison.OrdinalIgnoreCase)) return false;
            archiveFileName = Path.GetFileNameWithoutExtension(archiveFileName);

            // See if the name matches straight up
            if (modKey.Name.AsSpan().Equals(archiveFileName, StringComparison.OrdinalIgnoreCase)) return true;

            // Trim ending "type" information and try again
            var delimIndex = archiveFileName.LastIndexOf(" - ");
            if (delimIndex == -1) return false;

            return modKey.Name.AsSpan().Equals(archiveFileName.Slice(0, delimIndex), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Queries the related ini file and looks for Archive ordering information
        /// </summary>
        /// <param name="release">GameRelease to query for</param>
        /// <returns>Any Archive ordering info retrieved from the ini definition</returns>
        public static IEnumerable<string> GetIniListings(GameRelease release)
        {
            return GetIniListings(release, Ini.GetTypicalPath(release));
        }

        /// <summary>
        /// Queries the related ini file and looks for Archive ordering information
        /// </summary>
        /// <param name="release">GameRelease to query for</param>
        /// <param name="path">Path to the file containing INI data</param>
        /// <returns>Any Archive ordering info retrieved from the ini definition</returns>
        public static IEnumerable<string> GetIniListings(GameRelease release, string path)
        {
            return GetIniListings(release, FileSystem.File.OpenRead(path));
        }

        /// <summary>
        /// Queries the related ini file and looks for Archive ordering information
        /// </summary>
        /// <param name="release">GameRelease ini is for</param>
        /// <param name="iniStream">Stream containing INI data</param>
        /// <returns>Any Archive ordering info retrieved from the ini definition</returns>
        public static IEnumerable<string> GetIniListings(GameRelease release, Stream iniStream)
        {
            // Release exists as parameter, in case future games need different handling

            var parser = new FileIniDataParser();
            var data = parser.ReadData(new StreamReader(iniStream));
            var basePath = data["Archive"];
            var str1 = basePath["sResourceArchiveList"]?.Split(", ");
            var str2 = basePath["sResourceArchiveList2"]?.Split(", ");
            var ret = str1.EmptyIfNull().And(str2.EmptyIfNull()).ToList();
            return ret;
        }

        /// <summary>
        /// Creates a comparer that orders Archives to priority order, based on a given listed ordering.
        /// Any Archive not present in the listed Archives will go last <br/>
        /// <br/>
        /// If no order is given, then the typical ordering driven by the Ini is used.
        /// </summary>
        /// <param name="release">GameRelease to target</param>
        /// <param name="listedArchiveOrdering">Listed order of Archives, with higher priorty later in the list</param>
        /// <returns>Comparer that orders Archives in priority order</returns>
        private static IComparer<string>? GetPriorityOrderComparer(GameRelease release, IEnumerable<string>? listedArchiveOrdering = null)
        {
            return GetPriorityOrderComparer(listedArchiveOrdering ?? GetIniListings(release));
        }

        /// <summary>
        /// Creates a comparer that orders Archives to priority order, based on a given listed ordering.
        /// Any Archive not present in the listed Archives will go last
        /// </summary>
        /// <param name="listedArchiveOrdering">Listed order of Archives, with higher priorty later in the list</param>
        /// <returns>Comparer that orders Archives in priority order</returns>
        private static IComparer<string>? GetPriorityOrderComparer(IEnumerable<string> listedArchiveOrdering)
        {
            var archiveOrderingList = listedArchiveOrdering.ToList();
            if (archiveOrderingList.Count == 0) return null;
            archiveOrderingList.Reverse();
            return Comparer<string>.Create((a, b) =>
            {
                var indexA = archiveOrderingList.IndexOf(Path.GetFileName(a), (x, y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase));
                var indexB = archiveOrderingList.IndexOf(Path.GetFileName(b), (x, y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase));
                if (indexA == -1 && indexB == -1) return 0;
                if (indexA == -1) return 1;
                if (indexB == -1) return -1;
                return indexA - indexB;
            });
        }
    }
}

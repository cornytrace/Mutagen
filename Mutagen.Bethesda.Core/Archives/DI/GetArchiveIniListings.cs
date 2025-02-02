﻿using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using IniParser;
using IniParser.Model.Configuration;
using IniParser.Parser;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Inis;
using Noggog;

namespace Mutagen.Bethesda.Archives.DI
{
    public interface IGetArchiveIniListings
    {
        /// <summary>
        /// Queries the related ini file and looks for Archive ordering information
        /// </summary>
        /// <returns>Any Archive ordering info retrieved from the ini definition</returns>
        IEnumerable<FileName> Get();

        /// <summary>
        /// Queries the related ini file and looks for Archive ordering information
        /// </summary>
        /// <param name="path">Path to the file containing INI data</param>
        /// <returns>Any Archive ordering info retrieved from the ini definition</returns>
        IEnumerable<FileName> Get(FilePath path);

        /// <summary>
        /// Queries the related ini file and looks for Archive ordering information
        /// </summary>
        /// <param name="iniStream">Stream containing INI data</param>
        /// <returns>Any Archive ordering info retrieved from the ini definition</returns>
        IEnumerable<FileName> Get(Stream iniStream);
    }

    public class GetArchiveIniListings : IGetArchiveIniListings
    {
        private static readonly IniParserConfiguration Config = new()
        {
            AllowDuplicateKeys = true,
            AllowDuplicateSections = true,
            AllowKeysWithoutSection = true,
            AllowCreateSectionsOnFly = true,
            CaseInsensitive = true,
            SkipInvalidLines = true,
        };
        
        private readonly IFileSystem _fileSystem;
        private readonly IGameReleaseContext _gameReleaseContext;

        public GetArchiveIniListings(
            IFileSystem fileSystem,
            IGameReleaseContext gameReleaseContext)
        {
            _fileSystem = fileSystem;
            _gameReleaseContext = gameReleaseContext;
        }
        
        /// <inheritdoc />
        public IEnumerable<FileName> Get()
        {
            return Get(Ini.GetTypicalPath(_gameReleaseContext.Release));
        }

        /// <inheritdoc />
        public IEnumerable<FileName> Get(FilePath path)
        {
            return Get(_fileSystem.File.OpenRead(path.Path));
        }

        /// <inheritdoc />
        public IEnumerable<FileName> Get(Stream iniStream)
        {
            // Release exists as parameter, in case future games need different handling
            var parser = new FileIniDataParser(new IniDataParser(Config));
            var data = parser.ReadData(new StreamReader(iniStream));
            var basePath = data["Archive"];
            var str1 = basePath["sResourceArchiveList"]?.Split(", ");
            var str2 = basePath["sResourceArchiveList2"]?.Split(", ");
            var ret = str1.EmptyIfNull().And(str2.EmptyIfNull())
                .Select(x => new FileName(x))
                .ToList();
            return ret;
        }
    }
}
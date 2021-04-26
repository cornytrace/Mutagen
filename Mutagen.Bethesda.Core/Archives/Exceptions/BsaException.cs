using Noggog;
using System;

namespace Mutagen.Bethesda.Archives.Exceptions
{
    public class ArchiveException : Exception
    {
        public string? ArchiveFilePath { get; set; }
        public string? InternalFolderAccessed { get; set; }
        public string? InternalFileAccessed { get; set; }

        private ArchiveException(string message, Exception ex, string? bsaFilePath, string? folderAccessed, string? fileAccessed)
            : base(message, ex)
        {
            ArchiveFilePath = bsaFilePath;
            InternalFolderAccessed = folderAccessed;
            InternalFileAccessed = fileAccessed;
        }

        public static ArchiveException FileError(string message, Exception ex, string bsaFilePath, string fileAccessed)
        {
            if (ex is ArchiveException bsa) return bsa;
            return new ArchiveException(
                message,
                ex,
                bsaFilePath: bsaFilePath,
                folderAccessed: null,
                fileAccessed: fileAccessed);
        }

        public static ArchiveException FolderError(string message, Exception ex, string bsaFilePath, string folderAccessed)
        {
            if (ex is ArchiveException bsa) return bsa;
            return new ArchiveException(
                message,
                ex,
                bsaFilePath: bsaFilePath,
                folderAccessed: folderAccessed,
                fileAccessed: null);
        }

        public static ArchiveException OverallError(string message, Exception ex, string bsaFilePath)
        {
            if (ex is ArchiveException bsa) return bsa;
            return new ArchiveException(
                message,
                ex,
                bsaFilePath: bsaFilePath,
                folderAccessed: null,
                fileAccessed: null);
        }

        public override string ToString()
        {
            return $"{nameof(ArchiveException)} {ArchiveFilePath}{InternalFolderAccessed.Decorate(x => $"=>{x}")}{InternalFileAccessed.Decorate(x => $"=>{x}")}: {this.Message} {this.InnerException}{this.StackTrace}";
        }
    }
}

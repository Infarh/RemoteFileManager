using System.Collections.Generic;
using System.IO;

namespace RemoteFileManager.Interfaces.Services
{
    public interface IFileManagerService
    {
        IEnumerable<DriveInfo> GetRoots();

        IEnumerable<DirectoryInfo> GetDirectories(string Path);

        IEnumerable<FileInfo> GetFiles(string Path);

        FileInfo GetFileInfo(string Path);

        DirectoryInfo GetDirectoryInfo(string Path);

        void CopyFile(string Source, string Destination);

        void MoveFile(string Source, string Destination);

        void DeleteFile(string Path);

        DirectoryInfo CreateDirectory(string Path);

        void CopyDirectory(string Source, string Destination);

        void MoveDirectory(string Source, string Destination);

        void DeleteDirectory(string Path);

        int Execute(string Path);
    }
}

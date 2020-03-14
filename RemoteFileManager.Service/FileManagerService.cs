using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using RemoteFileManager.Interfaces.Services;

namespace RemoteFileManager.Service
{
    public class FileManagerService : IFileManagerService
    {
        public IEnumerable<DriveInfo> GetRoots() => DriveInfo.GetDrives();

        public IEnumerable<DirectoryInfo> GetDirectories(string Path) => new DirectoryInfo(Path).EnumerateDirectories();

        public IEnumerable<FileInfo> GetFiles(string Path) => new DirectoryInfo(Path).EnumerateFiles();

        public FileInfo GetFileInfo(string Path) => new FileInfo(Path);

        public DirectoryInfo GetDirectoryInfo(string Path) => new DirectoryInfo(Path);

        public void CopyFile(string Source, string Destination) => File.Copy(Source, Destination);

        public void MoveFile(string Source, string Destination) => File.Move(Source, Destination);

        public void DeleteFile(string Path) => File.Delete(Path);

        public DirectoryInfo CreateDirectory(string Path) => Directory.CreateDirectory(Path);

        public void DeleteDirectory(string Path) => Directory.Delete(Path, true);

        public int Execute(string Path, string Arguments, bool ShellExecute)
        {
            var info = new ProcessStartInfo(Path, Arguments) { UseShellExecute = ShellExecute };
            var process = Process.Start(info);
            return process.Id;
        }
    }
}

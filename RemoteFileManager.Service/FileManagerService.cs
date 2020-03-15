using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using RemoteFileManager.Interfaces.Services;

namespace RemoteFileManager.Service
{
    public class FileManagerService : IFileManagerService
    {
        private static void Log([CallerMemberName] string MethodName = null) => Console.WriteLine("{0}()", MethodName);

        public IEnumerable<DriveInfo> GetRoots()
        {
            Log();
            return DriveInfo.GetDrives();
        }

        public IEnumerable<DirectoryInfo> GetDirectories(string Path)
        {
            Log();
            return new DirectoryInfo(Path).EnumerateDirectories();
        }

        public IEnumerable<FileInfo> GetFiles(string Path)
        {
            Log();
            return new DirectoryInfo(Path).EnumerateFiles();
        }

        public FileInfo GetFileInfo(string Path)
        {
            Log();
            return new FileInfo(Path);
        }

        public DirectoryInfo GetDirectoryInfo(string Path)
        {
            Log();
            return new DirectoryInfo(Path);
        }

        public void CopyFile(string Source, string Destination)
        {
            Log();
            File.Copy(Source, Destination);
        }

        public void MoveFile(string Source, string Destination)
        {
            Log();
            File.Move(Source, Destination);
        }

        public void DeleteFile(string Path)
        {
            Log();
            File.Delete(Path);
        }

        public DirectoryInfo CreateDirectory(string Path)
        {
            Log();
            return Directory.CreateDirectory(Path);
        }

        public void DeleteDirectory(string Path)
        {
            Log();
            Directory.Delete(Path, true);
        }

        public int Execute(string Path, string Arguments, bool ShellExecute)
        {
            Log();
            var info = new ProcessStartInfo(Path, Arguments) { UseShellExecute = ShellExecute };
            var process = Process.Start(info);
            return process.Id;
        }
    }
}

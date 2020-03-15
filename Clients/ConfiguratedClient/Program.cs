using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RemoteFileManager.Interfaces.Services;

namespace ConfiguratedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new FileManager();

            client.GetRoots().ToList().ForEach(Console.WriteLine);
            Console.WriteLine(new string('-', Console.BufferWidth));

            client.GetRoots()
               .Select(r => r.RootDirectory)
               .SelectMany(d => client.GetDirectories(d.FullName))
               .ToList()
               .ForEach(Console.WriteLine);


            Console.WriteLine(new string('-', Console.BufferWidth));
            Console.ReadKey();
        }
    }

    class FileManager : ClientBase<IFileManagerService>, IFileManagerService
    {
        public IEnumerable<DriveInfo> GetRoots() => Channel.GetRoots();

        public IEnumerable<DirectoryInfo> GetDirectories(string Path) => Channel.GetDirectories(Path);

        public IEnumerable<FileInfo> GetFiles(string Path) => Channel.GetFiles(Path);

        public FileInfo GetFileInfo(string Path) => Channel.GetFileInfo(Path);

        public DirectoryInfo GetDirectoryInfo(string Path) => Channel.GetDirectoryInfo(Path);

        public void CopyFile(string Source, string Destination) => Channel.CopyFile(Source, Destination);

        public void MoveFile(string Source, string Destination) => Channel.MoveFile(Source, Destination);

        public void DeleteFile(string Path) => Channel.DeleteFile(Path);

        public DirectoryInfo CreateDirectory(string Path) => Channel.CreateDirectory(Path);

        public void DeleteDirectory(string Path) => Channel.DeleteDirectory(Path);

        public int Execute(string Path, string Arguments, bool ShellExecute) => Channel.Execute(Path, Arguments, ShellExecute);
    }
}

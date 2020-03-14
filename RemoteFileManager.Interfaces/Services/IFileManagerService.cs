using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace RemoteFileManager.Interfaces.Services
{
    [ServiceContract]
    public interface IFileManagerService
    {
        [OperationContract]
        IEnumerable<DriveInfo> GetRoots();

        [OperationContract]
        IEnumerable<DirectoryInfo> GetDirectories(string Path);

        [OperationContract]
        IEnumerable<FileInfo> GetFiles(string Path);

        [OperationContract]
        FileInfo GetFileInfo(string Path);

        [OperationContract]
        DirectoryInfo GetDirectoryInfo(string Path);

        [OperationContract]
        void CopyFile(string Source, string Destination);

        [OperationContract]
        void MoveFile(string Source, string Destination);

        [OperationContract]
        void DeleteFile(string Path);

        [OperationContract]
        DirectoryInfo CreateDirectory(string Path);

        [OperationContract]
        void DeleteDirectory(string Path);

        [OperationContract]
        int Execute(string Path, string Arguments, bool ShellExecute);
    }
}

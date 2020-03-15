using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using RemoteFileManager.FileServer;
using RemoteFileManager.Infrastructure.Commands;
using RemoteFileManager.Services.Interfaces;
using RemoteFileManager.ViewModels.Base;

namespace RemoteFileManager.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IFileServer _FileServer;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Клиент";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        #region Address : string - Адрес

        /// <summary>Адрес</summary>
        private string _Address = "http://localhost:8080/FileServer";

        /// <summary>Адрес</summary>
        public string Address { get => _Address; set => Set(ref _Address, value); }

        #endregion

        #region Drives : IEnumerable<DriveInfo> - Диски

        /// <summary>Диски</summary>
        private IEnumerable<DriveInfo> _Drives;

        /// <summary>Диски</summary>
        public IEnumerable<DriveInfo> Drives
        {
            get => _Drives;
            private set
            {
                if (!Set(ref _Drives, value)) return;
                SelectedDrive = null;
            }
        }

        #endregion

        #region SelectedDrive : DriveInfo - Выбранный диск

        /// <summary>Выбранный диск</summary>
        private DriveInfo _SelectedDrive;

        /// <summary>Выбранный диск</summary>
        public DriveInfo SelectedDrive
        {
            get => _SelectedDrive;
            set
            {
                if (!Set(ref _SelectedDrive, value)) return;
                SelectedDirectory = value?.RootDirectory;
            }
        }

        #endregion

        #region SelectedDirectory : DirectoryInfo - Выбранная директория

        /// <summary>Выбранная директория</summary>
        private DirectoryInfo _SelectedDirectory;

        /// <summary>Выбранная директория</summary>
        public DirectoryInfo SelectedDirectory
        {
            get => _SelectedDirectory;
            set
            {
                if (!Set(ref _SelectedDirectory, value)) return;
                if (value is null)
                {
                    SubDirectories = null;
                    Files = null;
                }
                else
                {
                    var client = new FileManagerServiceClient();
                    Set(ref _SubDirectories, client.GetDirectories(value.FullName), nameof(SubDirectories));
                    Set(ref _Files, client.GetFiles(value.FullName), nameof(Files));
                }
            }
        }

        #endregion

        #region SubDirectories : IEnumerable<DirectoryInfo> - Поддиректории выбранного каталога

        /// <summary>Поддиректории выбранного каталога</summary>
        private IEnumerable<DirectoryInfo> _SubDirectories;

        /// <summary>Поддиректории выбранного каталога</summary>
        public IEnumerable<DirectoryInfo> SubDirectories { get => _SubDirectories; private set => Set(ref _SubDirectories, value); }

        #endregion

        #region Files : IEnumerable<FileInfo> - Файлы выбранного каталога

        /// <summary>Файлы выбранного каталога</summary>
        private IEnumerable<FileInfo> _Files;

        /// <summary>Файлы выбранного каталога</summary>
        public IEnumerable<FileInfo> Files
        {
            get => _Files;
            private set
            {
                if (!Set(ref _Files, value)) return;
                SelectedFile = null;
            }
        }

        #endregion

        #region SelectedFile : FileInfo - Выбранный файл

        /// <summary>Выбранный файл</summary>
        private FileInfo _SelectedFile;

        /// <summary>Выбранный файл</summary>
        public FileInfo SelectedFile
        {
            get => _SelectedFile;
            set => Set(ref _SelectedFile, value);
        }

        #endregion

        #region Команды

        #region Command GetDrivesCommand - Получить диски

        /// <summary>Получить диски</summary>
        public ICommand GetDrivesCommand { get; }

        /// <summary>Проверка возможности выполнения - Получить диски</summary>
        private static bool CanGetDrivesCommandExecute() => true;

        /// <summary>Логика выполнения - Получить диски</summary>
        private void OnGetDrivesCommandExecuted() => Drives = new FileManagerServiceClient().GetRoots();

        #endregion

        #region Command GoToParentDirectoryCommand - Переместиться вверх по дереву каталогов

        /// <summary>Переместиться вверх по дереву каталогов</summary>
        public ICommand GoToParentDirectoryCommand { get; }

        /// <summary>Проверка возможности выполнения - Переместиться вверх по дереву каталогов</summary>
        private static bool CanGoToParentDirectoryCommandExecute(object p) => p is DirectoryInfo dir && dir.Parent != null;

        /// <summary>Логика выполнения - Переместиться вверх по дереву каталогов</summary>
        private void OnGoToParentDirectoryCommandExecuted(object p) => SelectedDirectory = (p as DirectoryInfo)?.Parent;

        #endregion

        #region Command ExecuteFileCommand - Выполнить файл

        /// <summary>Выполнить файл</summary>
        public ICommand ExecuteFileCommand { get; }

        /// <summary>Проверка возможности выполнения - Выполнить файл</summary>
        private static bool CanExecuteFileCommandExecute(object p) => p is FileInfo file && file.Exists;

        /// <summary>Логика выполнения - Выполнить файл</summary>
        private static void OnExecuteFileCommandExecuted(object p)
        {
            new FileManagerServiceClient().Execute(((FileInfo) p).FullName, string.Empty, true);
        }

        #endregion

        #endregion

        public MainWindowViewModel(IFileServer FileServer)
        {
            _FileServer = FileServer;

            #region Команды

            GetDrivesCommand = new LambdaCommand(OnGetDrivesCommandExecuted, CanGetDrivesCommandExecute);
            GoToParentDirectoryCommand = new LambdaCommand(OnGoToParentDirectoryCommandExecuted, CanGoToParentDirectoryCommandExecute);
            ExecuteFileCommand = new LambdaCommand(OnExecuteFileCommandExecuted, CanExecuteFileCommandExecute);

            #endregion
        }
    }
}

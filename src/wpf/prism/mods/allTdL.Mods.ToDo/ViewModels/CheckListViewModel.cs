using System;
using System.Collections.ObjectModel;
using System.IO;
using allTdL.Core.Mvvm;
using allTdL.Mvvm;
using allTdL.Services.Interfaces;

using Prism.Regions;

namespace allTdL.Mods.ToDo.ViewModels
{
    public class CopyDir
    {
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }

    public class CheckListViewModel : NavigationViewModelBase
    {
        private ObservableCollection<DirectoryInfo> _items;
        public CheckListViewModel(IRegionManager regionManager, IMessageService messageService) : base(regionManager)
        {
                //DriveInfo driveInfo = new DriveInfo()
                Items = new ObservableCollection<DirectoryInfo>();

                var drives = messageService.GetAllDriveInfo();

                foreach (var driveInfo in drives)
                {
                    
                    Items.Add(driveInfo.RootDirectory);
                }
                
                Console.WriteLine();
        }

        public ObservableCollection<DirectoryInfo> Items { get { return _items; } set {_items = value;} }

        private object _selectedItem;

        private string _sourceDirectory;
        public string SourceDirectory
        {
            get { return _sourceDirectory; }
            set { SetProperty(ref _sourceDirectory, value); }
        }


        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                SelectedFileName = SelectedItem?.ToString();
            }
        }


        void GetChildItems()
        {
            var dirInfo = new DirectoryInfo(SelectedItem.ToString());
            //dirInfo.EnumerateFiles()
        }

        static void DisplayFileSystemInfoAttributes(FileSystemInfo fsi)
        {
            //  Assume that this entry is a file.
            var entryType = "File";

            // Determine if entry is really a directory
            if (IsDirectory(fsi))
            {
                entryType = "Directory";
            }
            //  Show this entry's type, name, and creation date.
            Console.WriteLine("{0} entry {1} was created on {2:D}", entryType, fsi.FullName, fsi.CreationTime);
        }

        static bool IsDirectory(FileSystemInfo fsi)
        {
            if ((fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory )
            {
                return true;
            }

            return false;
        }

        static bool IsDirectory(FileInfo fi)
        {
            if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory )
            {
                return true;
            }

            return false;
        }

        static void DisplayFileInfoAttributes(FileInfo fi)
        {
            //  Assume that this entry is a file.
            string entryType = "File";

            // Determine if entry is really a directory
            if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory )
            {
                entryType = "Directory";
            }
            //  Show this entry's type, name, and creation date.
            Console.WriteLine("{0} entry {1} was created on {2:D}", entryType, fi.FullName, fi.CreationTime);
        }


        private string _selectedFileName;
        public string SelectedFileName
        {
            get { return _selectedFileName; }
            set { SetProperty(ref _selectedFileName, value); }
        }




        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something

            //Items.Add("Test");
            //Items.Add("Test2");
            SourceDirectory = "C:\\";
        }
    }
}

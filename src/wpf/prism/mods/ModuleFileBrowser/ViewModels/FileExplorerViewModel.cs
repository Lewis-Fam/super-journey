using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ModuleFileBrowser.Models;

namespace ModuleFileBrowser.ViewModels
{
    public class FileExplorerViewModel : FileInfoModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileExplorerViewModel"/> class.
        /// </summary>
        public FileExplorerViewModel()
        {
            this.DriveDetails = this.GetRootDrives();
        }


        private List<FileInfoModel> _driveDetails;
        /// <summary>
        /// Get or set the DriveDetails
        /// </summary>
        public List<FileInfoModel> DriveDetails
        {
            get { return _driveDetails; }
            set { _driveDetails = value; }
        }
        /// <summary>
        /// Gets the root drives.
        /// </summary>
        /// <returns></returns>
        public List<FileInfoModel> GetRootDrives()
        {
            List<FileInfoModel> drives = new List<FileInfoModel>();
            foreach (string s in Directory.GetLogicalDrives())
            {
                try
                {
                    drives.Add(infomodel(s));
                }
                catch (Exception) { }
            }
            return drives;
        }
        /// <summary>
        /// Used to get ChildFolder Content of Particular folder
        /// </summary>
        /// <param name="fileNodeItem"></param>
        /// <returns></returns>
        public IEnumerable<FileInfoModel> GetChildFolderContent(FileInfoModel fileNodeItem)
        {
            var rtnChildren = new List<FileInfoModel>();

            var folder = fileNodeItem.FullName;

            try
            {
                var fi = new FileInfo(folder);
                if ((fi.Attributes & FileAttributes.Directory) != (FileAttributes)0)
                {
                    var di = new DirectoryInfo(folder);
                    // Skip Recycle Bin, System Volume Information etc.
                    if (di.Parent != null && (di.Attributes & FileAttributes.Hidden) != (FileAttributes)0
                        || (int)di.Attributes == -1)
                    {
                        //skip...
                    }
                    else
                    {
                        foreach (var s in Directory.GetDirectories(folder))
                        {
                            var fi2 = new FileInfo(s);
                            if ((fi2.Attributes & FileAttributes.Hidden) != (FileAttributes)0)
                                continue;
                            rtnChildren.Add(infomodel(s));
                        }
                        foreach (var s in Directory.GetFiles(folder))
                        {
                            var fi2 = new FileInfo(s);
                            if ((fi2.Attributes & FileAttributes.Hidden) != (FileAttributes)0)
                                continue;
                            rtnChildren.Add(infomodel(s));
                        }
                    }
                }
            }
            catch { }
            return rtnChildren;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoModel"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        private static FileInfoModel infomodel(string path)
        {
            FileInfoModel model = new FileInfoModel();
            FileInfo fi = new FileInfo(path);
            model.FullName = Path.GetFullPath(path);
            model.ShortName = Path.GetFileNameWithoutExtension(path);
            if (model.ShortName == "")
            {
                model.ShortName = path;
                model.FileType = "DriveNode";
                System.IO.DriveInfo di = new System.IO.DriveInfo(path);
                model.TotalSize = (di.TotalSize / 1073741824).ToString();
                var freeSpace = (double.Parse(di.TotalFreeSpace.ToString()) / 1073741824);
                model.TotalFreeSpace = (Math.Round(freeSpace, 1)).ToString(CultureInfo.InvariantCulture);
                model.PercentofFreeSpace = 100 - ((double.Parse(model.TotalFreeSpace) / double.Parse(model.TotalSize)) * 100);
            }
            else
            {
                if ((fi.Attributes & FileAttributes.Directory) != 0)
                {
                    model.FileType = "Directory";
                }
                else
                {
                    model.Size = fi.Length + "Kb";
                    model.FileType = Path.GetExtension(path);
                }
            }
            model.DateModified = fi.LastWriteTime;
            model.DateAccessed = fi.LastAccessTime;
            model.DateCreated = fi.CreationTime;
            model.Attributes = fi.Attributes;
            return model;
        }
    }
}
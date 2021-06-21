using System;
using System.IO;
using allTdL.Core.Mvvm;

namespace ModuleFileBrowser.Models
{
    /// <summary>
    /// File info model.
    /// </summary>
    public class FileInfoModel : BindableObject
    {
        string _shortName;
        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>The short name.</value>
        public string ShortName
        {
            get => _shortName;
            set
            {
                _shortName = value;
                RaisePropertyChanged(nameof(ShortName));
            }
        }

        DateTime _dataModified;
        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>The date modified.</value>
        public DateTime DateModified
        {
            get => _dataModified;
            set
            {
                _dataModified = value;
                RaisePropertyChanged(nameof(DateModified));
            }
        }

        string _fileType;
        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        public string FileType
        {
            get => _fileType;
            set
            {
                _fileType = value;
                RaisePropertyChanged(nameof(FileType));
            }
        }

        string _size;
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                RaisePropertyChanged(nameof(Size));
            }
        }

        string _totalSize;
        /// <summary>
        /// Gets or sets the total size.
        /// </summary>
        /// <value>The total size.</value>
        public string TotalSize
        {
            get => _totalSize;
            set
            {
                _totalSize = value;
                RaisePropertyChanged(nameof(TotalSize));
            }
        }

        string _totalFreeSpace;
        /// <summary>
        /// Gets or sets the total free space.
        /// </summary>
        /// <value>The total free space.</value>
        public string TotalFreeSpace
        {
            get => _totalFreeSpace;
            set
            {
                _totalFreeSpace = value;
                RaisePropertyChanged(nameof(TotalFreeSpace));
            }
        }

        double _percentofFreeSpace;
        /// <summary>
        /// Gets or sets the percentof free space.
        /// </summary>
        /// <value>The percentof free space.</value>
        public double PercentofFreeSpace
        {
            get => _percentofFreeSpace;
            set
            {
                _percentofFreeSpace = value;
                RaisePropertyChanged(nameof(PercentofFreeSpace));
            }
        }

        string _fullName;
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }

        DateTime _dateCreated;
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>The date created.</value>
        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
                RaisePropertyChanged(nameof(DateCreated));
            }
        }

        FileAttributes _attributes;
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public FileAttributes Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                RaisePropertyChanged(nameof(DateCreated));
            }
        }

        DateTime _dateAccessed;
        /// <summary>
        /// Gets or sets the date accessed.
        /// </summary>
        /// <value>The date accessed.</value>
        public DateTime DateAccessed
        {
            get => _dateAccessed;
            set
            {
                _dateAccessed = value;
                RaisePropertyChanged(nameof(DateAccessed));
            }
        }
    }
}
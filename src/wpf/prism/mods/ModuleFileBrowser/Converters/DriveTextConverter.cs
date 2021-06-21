using System;
using System.Windows.Data;
using FileInfoModel = ModuleFileBrowser.Models.FileInfoModel;

namespace ModuleFileBrowser.Converters
{
    public class DriveTextConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is FileInfoModel fileNode)
            {
                return fileNode.TotalFreeSpace + " GB free of " + fileNode.TotalSize + " GB";
            }

            var val = value?.ToString();
            if (!string.IsNullOrEmpty(val))
            {
                string[] st = val.Split('\\');
                return "Local Disc (" + st[0] + ")";
            }
            return "";
        }
        
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
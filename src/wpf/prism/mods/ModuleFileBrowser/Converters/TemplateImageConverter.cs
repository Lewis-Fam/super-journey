using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ModuleFileBrowser.Models;
using Syncfusion.UI.Xaml.TreeGrid.Cells;

namespace ModuleFileBrowser.Converters
{
    public class TemplateImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dataContextHelper = value as TreeGridDataContextHelper;           
            var record = dataContextHelper.Record as FileInfoModel;
            if (record.FileType == "DriveNode")
                return new BitmapImage(new Uri(@"/ModuleFileBrowser;component/Assets/treegrid/DriveNode.png", UriKind.Relative));
            else if (record.FileType == "Directory")
                return new BitmapImage(new Uri(@"/ModuleFileBrowser;component/Assets/treegrid/Directory.png", UriKind.Relative));
            //return new BitmapImage(new Uri(@"/syncfusion.treegriddemos.wpf;component/Assets/treegrid/Directory.png", UriKind.Relative));
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
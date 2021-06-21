using System.Windows;
using System.Windows.Media.Imaging;

namespace allTdL.Core.Mvvm.Converters
{  
    /// <summary>
    /// This class converts a collection size to visibility.
    /// </summary>
    public class CollectionVisibilityConverter : EmptyCollectionToObjectConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionVisibilityConverter"/> class.
        /// </summary>
        public CollectionVisibilityConverter()
        {
            NotEmptyValue = Visibility.Visible;
            EmptyValue = Visibility.Collapsed;
        }
    }

}

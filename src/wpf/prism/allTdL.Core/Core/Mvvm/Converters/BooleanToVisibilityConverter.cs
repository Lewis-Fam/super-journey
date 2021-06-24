using System.Windows;

namespace allTdL.Core.Mvvm.Converters
{
    /// <summary>
    /// Convert between boolean and visibility
    /// </summary>
    public sealed class BooleanToVisibilityConverter : BoolToObjectConverter
    {
        public BooleanToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}
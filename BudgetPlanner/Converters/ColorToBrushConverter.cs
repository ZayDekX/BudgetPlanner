using System;

using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BudgetPlanner.Converters
{
    internal class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not Color color)
            {
                return new SolidColorBrush();
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is not SolidColorBrush brush)
            {
                return default(Color);
            }

            return brush.Color;
        }
    }
}

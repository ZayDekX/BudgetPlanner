using System;

using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BudgetPlanner.Converters;

internal class ColorToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not System.Drawing.Color color)
        {
            return new SolidColorBrush();
        }

        return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is not SolidColorBrush brush)
        {
            return default(Color);
        }

        return System.Drawing.Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
    }
}

using System;

using Windows.UI.Xaml.Data;

namespace BudgetPlanner.Converters;

public class StringToFloatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is not string str)
        {
            return default;
        }

        if (string.IsNullOrEmpty(str))
        {
            return 0f;
        }

        return float.Parse(str);
    }
}

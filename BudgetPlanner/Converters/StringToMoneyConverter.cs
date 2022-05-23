using System;

using BudgetPlanner.Data;

using Windows.UI.Xaml.Data;

namespace BudgetPlanner.Converters
{
    public class StringToMoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((Money)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is not string str)
            {
                return default;
            }

            if (string.IsNullOrEmpty(str))
            {
                return Money.Zero(Settings.Instance.CurrencyMarker);
            }

            var i = str.IndexOf(' ');

            if(i < 0)
            {
                i = str.Length - 1;
            }

            return new Money(double.Parse(str.Substring(0, i)), Settings.Instance.CurrencyMarker);
        }
    }
}

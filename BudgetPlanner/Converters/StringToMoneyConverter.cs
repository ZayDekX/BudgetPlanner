using System;

using BudgetPlanner.Data;

using Windows.UI.Xaml.Data;

namespace BudgetPlanner.Converters
{
    public class StringToMoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is null)
            {
                return "0";
            }

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
                return Money.Zero(Settings.CurrencyMarker);
            }

            var i = str.IndexOf(' ');

            if(i <= 0)
            {
                i = str.Length;
            }

            return new Money(double.Parse(str.Substring(0, i)), Settings.CurrencyMarker);
        }
    }
}

using System;

using BudgetPlanner.Data;

using Windows.UI.Xaml.Data;

namespace BudgetPlanner.Converters;

public class StringToMoneyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not Money money)
        {
            return "0";
        }

        if (parameter is true)
        {
            return money.ToString();
        }

        return money.Amount.ToString("n2") + Settings.CurrencyMarker;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        var str = value as string;

        if (string.IsNullOrEmpty(str))
        {
            return Money.Zero;
        }

        var i = str.IndexOf(' ');

        if (i <= 0)
        {
            i = str.Length;
        }

        return new Money(double.Parse(str.Substring(0, i)));
    }
}

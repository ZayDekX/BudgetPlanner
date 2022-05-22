using System;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views
{
    public abstract class BasePage : UserControl
    {
        public string NavigationTitle { get; set; }

        public string NavigationIconSymbol { get; set; }

        public IconElement NavigationIcon => new SymbolIcon(Enum.Parse<Symbol>(NavigationIconSymbol));
    }
}

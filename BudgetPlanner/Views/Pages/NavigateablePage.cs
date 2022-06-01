using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Pages
{
    public abstract class NavigateablePage : Page
    {
        public PageNavigator Navigator { get; protected set; }
    }
}

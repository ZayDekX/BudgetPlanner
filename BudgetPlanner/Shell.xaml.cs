using BudgetPlanner.ViewModels;
using BudgetPlanner.Views;

using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BudgetPlanner
{
    public sealed partial class Shell
    {
        private BudgetPlannerViewModel ViewModel { get; } = new();

        public Shell()
        {
            InitializeComponent();
        }

        private void OnNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not NavigationView navigationView)
            {
                return;
            }

            navigationView.SelectedItem = navigationView.MenuItems[0];
        }

        private void NavigateToSelectedPage(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not PageNavigator navigator)
            {
                return;
            }

            RootContainer.Navigate(navigator.PageType);
        }
    }
}

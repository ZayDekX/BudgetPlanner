using BudgetPlanner.Views;
using BudgetPlanner.Views.Pages;

using Microsoft.UI.Xaml.Controls;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace BudgetPlanner;

public sealed partial class Shell
{
    public Shell()
    {
        InitializeComponent();
    }

    private bool _isExternalNavigation;

    public void Navigate<TPage>(object args = default)
        where TPage : NavigateablePage
    {
        _isExternalNavigation = true;
        RootContainer.Navigate(typeof(TPage), args, new DrillInNavigationTransitionInfo());

        Navigation.SelectedItem = ((TPage)RootContainer.Content).Navigator;
        _isExternalNavigation = false;
    }

    public void GoBack()
    {
        if (RootContainer.CanGoBack)
        {
            RootContainer.GoBack();

            if (RootContainer.Content is NavigateablePage navPage)
            {
                Navigation.SelectedItem = navPage.Navigator;
            }
        }
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

        if (!_isExternalNavigation)
        {
            RootContainer.Navigate(navigator.PageType);
        }
    }
}

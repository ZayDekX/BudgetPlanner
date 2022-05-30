using BudgetPlanner.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BudgetPlanner
{
    public sealed partial class MainPage : Page
    {
        private BudgetPlannerViewModel ViewModel { get; } = new();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not Microsoft.UI.Xaml.Controls.NavigationView navigationView)
            {
                return;
            }

            navigationView.SelectedItem = navigationView.MenuItems[0];
        }
    }
}

using BudgetPlanner.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BudgetPlanner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private BudgetPlannerViewModel ViewModel { get; } = new();

        public MainPage()
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
    }
}

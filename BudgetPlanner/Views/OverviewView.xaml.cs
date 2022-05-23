using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Views
{
    public sealed partial class OverviewView
    {
        public OverviewViewModel ViewModel { get; } = new(StatsProvider.Default);

        public OverviewView()
        {
            InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}

using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Views
{
    public sealed partial class OverviewView
    {
        public OverviewViewModel ViewModel { get; } = new(DataProvider.Instance);

        public OverviewView()
        {
            InitializeComponent();
            Loaded += (_, _) => ViewModel.Update();
        }
    }
}

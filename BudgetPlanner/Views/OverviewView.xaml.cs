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
    }
}

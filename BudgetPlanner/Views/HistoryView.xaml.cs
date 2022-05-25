using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Views
{
    public sealed partial class HistoryView
    {
        public HistoryViewModel ViewModel { get; } = new(DataProvider.Instance);

        public HistoryView()
        {
            InitializeComponent();
            Loaded += (_, _) => ViewModel.Update();
        }
    }
}

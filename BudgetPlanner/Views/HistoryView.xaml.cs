using BudgetPlanner.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace BudgetPlanner.Views
{
    public sealed partial class HistoryView
    {
        public HistoryViewModel ViewModel { get; } = HistoryViewModel.Instance;

        public HistoryView()
        {
            InitializeComponent();
        }
    }
}

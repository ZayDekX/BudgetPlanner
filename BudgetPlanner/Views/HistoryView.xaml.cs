using BudgetPlanner.ViewModels;

using Windows.UI.Xaml;

namespace BudgetPlanner.Views
{
    public sealed partial class HistoryView
    {
        public HistoryViewModel ViewModel { get; } = new();

        public HistoryView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Update();
        }
    }
}

using BudgetPlanner.ViewModels;

using Windows.UI.Xaml;

namespace BudgetPlanner.Views
{
    public sealed partial class OperationCreatorView
    {
        private OperationCreatorViewModel ViewModel { get; } = new();

        public OperationCreatorView()
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

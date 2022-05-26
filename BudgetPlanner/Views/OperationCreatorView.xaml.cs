using System;

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
            Loaded += Update;
        }

        private async void Update(object sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, ViewModel.Update);
        }
    }
}

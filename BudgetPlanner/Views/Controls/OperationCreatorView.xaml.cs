using System;

using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Views.Controls
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

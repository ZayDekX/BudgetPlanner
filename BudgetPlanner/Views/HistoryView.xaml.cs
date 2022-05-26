using System;

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
            Loaded += Update;
        }

        private async void Update(object sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, ViewModel.Update);
        }
    }
}

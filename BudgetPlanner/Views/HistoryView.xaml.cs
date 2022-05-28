using System;

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
            Loaded += Update;
        }

        private async void Update(object sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, ViewModel.Update);
        }
    }
}

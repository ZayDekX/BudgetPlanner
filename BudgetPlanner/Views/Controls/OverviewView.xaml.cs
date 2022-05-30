using System;

using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;

using Windows.UI.Core;

namespace BudgetPlanner.Views.Controls
{
    public sealed partial class OverviewView
    {
        public OverviewViewModel ViewModel { get; } = new(DataProvider.Instance);

        public OverviewView()
        {
            InitializeComponent();
            Loaded += Update;
        }

        private async void Update(object sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ViewModel.Update);
        }
    }
}

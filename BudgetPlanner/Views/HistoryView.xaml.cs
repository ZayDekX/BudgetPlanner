using System;

using Windows.UI.Core;

using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;
using Windows.UI.Xaml.Controls;

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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ViewModel.Update);
        }

        private async void CalendarViewSelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ViewModel.UpdateSelectedPeriod(sender, args));
        }
    }
}

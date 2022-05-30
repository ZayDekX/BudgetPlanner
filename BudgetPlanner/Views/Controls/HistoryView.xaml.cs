using System;
using System.Threading.Tasks;

using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;

using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls
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

        private async void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            await DisplayDeleteFileDialog();
        }

        private async Task DisplayDeleteFileDialog()
        {
            var deleteFileDialog = new ContentDialog
            {
                Title = "Delete selected operation permanently?",
                Content = "If you delete this operation, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            var result = await deleteFileDialog.ShowAsync();

            if (result is ContentDialogResult.Primary)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ViewModel.DeleteSelectedOperation);
            }
        }
    }
}

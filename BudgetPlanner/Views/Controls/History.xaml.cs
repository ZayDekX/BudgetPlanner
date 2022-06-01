using System;
using System.Linq;
using System.Threading.Tasks;

using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;
using BudgetPlanner.Views.Pages;

using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls
{
    public sealed partial class History
    {
        public HistoryViewModel ViewModel { get; } = new(DataProvider.Instance);

        public History()
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

        private void OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            App.CurrentShell.Navigate<EditOperationPage>(ViewModel.SelectedOperation);
        }

        public string PeriodSelectorText { get; set; }

        private bool _updatingSelectedDates;
        private bool _updatedSelectedDates;
        private bool _allowAllOperations;

        private DateTimeOffset _startDate;
        private DateTimeOffset _endDate;

        public void UpdateSelectedPeriod(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            switch (sender.SelectedDates.Count)
            {
                case 0 when _updatingSelectedDates:
                    sender.SelectedDates.Add(_startDate);
                    return;
                case 1 when _updatingSelectedDates:
                    _updatedSelectedDates = true;
                    sender.SelectedDates.Add(_endDate);
                    return;
                case 2 when _updatedSelectedDates:
                    _updatingSelectedDates = false;
                    _startDate = sender.SelectedDates.Min();
                    _endDate = sender.SelectedDates.Max();
                    break;

                case 0:
                    PeriodSelectorText = "Select period";
                    _allowAllOperations = true;
                    ViewModel.Update();
                    return;

                case 1:
                    _allowAllOperations = false;

                    var dates = new[] { sender.SelectedDates.First(), DateTime.Today };

                    _startDate = dates.Min();
                    _endDate = dates.Max();
                    break;

                default:
                    _startDate = sender.SelectedDates.Min();
                    _endDate = sender.SelectedDates.Max();

                    _updatingSelectedDates = true;
                    _updatedSelectedDates = false;
                    _allowAllOperations = false;

                    sender.SelectedDates.Clear();
                    return;
            }

            PeriodSelectorText = $"{_startDate:dd.MM.yyyy}-{_endDate:dd.MM.yyyy}";
            ViewModel.Update();
        }
    }
}

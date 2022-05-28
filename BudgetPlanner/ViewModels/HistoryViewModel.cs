using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI.Xaml.Controls;
using System.Linq;

namespace BudgetPlanner.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        private OperationViewModel _selectedOperation;

        public static HistoryViewModel Instance { get; } = new();

        public ObservableCollection<IGrouping<DateTime, OperationViewModel>> AvailableOperations
        {
            get => _availableOperations;
            set => SetProperty(ref _availableOperations, value);
        }

        public OperationViewModel SelectedOperation
        {
            get => _selectedOperation;
            set => SetProperty(ref _selectedOperation, value);
        }

        public string PeriodSelectorText
        {
            get => _periodSelectorText;
            set => SetProperty(ref _periodSelectorText, value);
        }

        private string _periodSelectorText = "Period";

        private DateTimeOffset _startDate;
        private DateTimeOffset _endDate;

        private bool _updatingSelectedDates;
        private bool _updatedSelectedDates;
        private bool _allowAllOperations = true;
        private ObservableCollection<IGrouping<DateTime, OperationViewModel>> _availableOperations;

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
                    PeriodSelectorText = "Period";
                    _allowAllOperations = true;
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
            Update();
        }

        public void Update()
        {
            AvailableOperations = new(FilterOperations(Array.Empty<OperationViewModel>()));
        }

        private IEnumerable<IGrouping<DateTime, OperationViewModel>> FilterOperations(IEnumerable<OperationViewModel> operations)
        {
            var operationsInRange = operations.Where(o => _allowAllOperations || o.DateTime >= _startDate && o.DateTime <= _endDate).OrderByDescending(o => o.DateTime);
            var groupedOperations = operationsInRange.GroupBy(o => o.DateTime);

            return groupedOperations;
        }
    }
}

using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using BudgetPlanner.Data;
using BudgetPlanner.Providers;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        public HistoryViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private OperationViewModel _selectedOperation;
        private ObservableCollection<IGrouping<DateTime, OperationViewModel>> _availableOperations = new();

        private readonly IDataProvider _dataProvider;

        private string _periodSelectorText = "Period";

        private DateTimeOffset _startDate;
        private DateTimeOffset _endDate;

        private bool _updatingSelectedDates;
        private bool _updatedSelectedDates;
        private bool _allowAllOperations = true;

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
                    Update();
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
            var operations = _dataProvider.GetOperations().Select(o => new OperationViewModel(o));

            AvailableOperations = new(FilterOperations(operations));
        }

        private IEnumerable<IGrouping<DateTime, OperationViewModel>> FilterOperations(IEnumerable<OperationViewModel> operations)
        {
            return operations
                .Where(o => _allowAllOperations || o.DateTime >= _startDate && o.DateTime <= _endDate)
                .OrderByDescending(o => o.DateTime)
                .GroupBy(o => o.DateTime);
        }
    }
}

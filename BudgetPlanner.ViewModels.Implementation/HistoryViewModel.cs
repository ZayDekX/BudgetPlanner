using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Utils.Commands;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels.Implementation;

public class HistoryViewModel : ObservableObject, IHistoryViewModel
{
    public HistoryViewModel(IOperationProvider operationProvider)
    {
        UpdateCommand = new DispatcherCommand(Update);
        UpdateDateRangeCommand = new DispatcherCommand<(DateTimeOffset, DateTimeOffset)>(UpdateDateRange);
        DeselectRangeCommand = new DispatcherCommand(DeselectRange);
        DeleteSelectedOperationCommand = new DispatcherCommand(DeleteSelectedOperation);

        _operationProvider = operationProvider;
    }

    private IOperationViewModel _selectedOperation;
    private ObservableCollection<IGrouping<DateTime, IOperationViewModel>> _availableOperations = new();

    public ICommand UpdateCommand { get; }

    public ICommand DeleteSelectedOperationCommand { get; }

    public ICommand DeselectRangeCommand { get; }

    public ICommand UpdateDateRangeCommand { get; }

    private readonly IOperationProvider _operationProvider;

    private DateTimeOffset _startDate;
    private DateTimeOffset _endDate;
    private bool _showAllOperations = true;
    private string _periodSelectorText = "Period";

    public ObservableCollection<IGrouping<DateTime, IOperationViewModel>> AvailableOperations
    {
        get => _availableOperations;
        set => SetProperty(ref _availableOperations, value);
    }

    public IOperationViewModel SelectedOperation
    {
        get => _selectedOperation;
        set => SetProperty(ref _selectedOperation, value);
    }

    public string PeriodSelectorText
    {
        get => _periodSelectorText;
        set => SetProperty(ref _periodSelectorText, value);
    }

    private void Update()
    {
        var operations = _operationProvider.GetOperations().Select(o => new OperationViewModel(o));

        AvailableOperations = new(FilterOperations(operations));
    }

    private IEnumerable<IGrouping<DateTime, IOperationViewModel>> FilterOperations(IEnumerable<IOperationViewModel> operations)
    {
        return operations
            .Where(o => _showAllOperations || o.DateTime.Date >= _startDate.Date && o.DateTime.Date <= _endDate.Date)
            .OrderByDescending(o => o.DateTime)
            .GroupBy(o => o.DateTime.Date);
    }

    private void UpdateDateRange((DateTimeOffset startDate, DateTimeOffset endDate) args)
    {
        (_startDate, _endDate) = args;
        _showAllOperations = false;

        PeriodSelectorText = $"{_startDate:dd.MM.yyyy}-{_endDate:dd.MM.yyyy}";
        Update();
    }

    private void DeselectRange()
    {
        _showAllOperations = true;

        PeriodSelectorText = "Period";
        Update();
    }

    private void DeleteSelectedOperation()
    {
        _operationProvider.Delete(SelectedOperation.Id);
        SelectedOperation = null;

        Update();
    }
}

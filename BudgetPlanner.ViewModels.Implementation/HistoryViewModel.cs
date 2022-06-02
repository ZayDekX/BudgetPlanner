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
    public HistoryViewModel(IOperationProvider dataProvider)
    {
        UpdateCommand = new DispatcherCommand(Update);
        _operationProvider = dataProvider;
    }

    private IOperationViewModel _selectedOperation;
    private ObservableCollection<IGrouping<DateTime, IOperationViewModel>> _availableOperations = new();

    public ICommand UpdateCommand { get; }

    private readonly IOperationProvider _operationProvider;

    private DateTimeOffset _startDate;
    private DateTimeOffset _endDate;

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

    private void Update()
    {
        var operations = _operationProvider.GetOperations().Select(o => new OperationViewModel(o));

        AvailableOperations = new(FilterOperations(operations));
    }

    private IEnumerable<IGrouping<DateTime, IOperationViewModel>> FilterOperations(IEnumerable<IOperationViewModel> operations)
    {
        return operations
            .Where(o => o.DateTime >= _startDate && o.DateTime <= _endDate)
            .OrderByDescending(o => o.DateTime)
            .GroupBy(o => o.DateTime);
    }

    public void DeleteSelectedOperation()
    {
        _operationProvider.Delete(SelectedOperation.Id);
        SelectedOperation = null;

        Update();
    }
}

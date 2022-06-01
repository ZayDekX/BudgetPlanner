using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BudgetPlanner.DataAccess;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels;

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

    public void DeleteSelectedOperation()
    {
        _dataProvider.DeleteOperation(SelectedOperation);
        SelectedOperation = null;

        Update();
    }
}

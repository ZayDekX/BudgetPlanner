using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BudgetPlanner.Commands;
using BudgetPlanner.Data;
using BudgetPlanner.Models;
using BudgetPlanner.Providers;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels;

internal class OperationCreatorViewModel : ObservableObject
{
    public OperationCreatorViewModel(IDataProvider dataProvider)
    {
        ValidateAndCreateOperationCommand = new DispatcherCommand(CreateOperation);
        UpdateCommand = new DispatcherCommand(Update);

        Date = DateTime.Today;
        Time = DateTime.Now - DateTime.Today;
        _dataProvider = dataProvider;
    }
    
    private Money _amount;
    private string _comment;
    private OperationCategory _category;
    private DateTimeOffset _date;
    private TimeSpan _time;
    private ObservableCollection<OperationCategory> _availableOperationCategories = new();
    private readonly IDataProvider _dataProvider;

    public ObservableCollection<OperationCategory> AvailableOperationCategories
    {
        get => _availableOperationCategories;
        set => SetProperty(ref _availableOperationCategories, value);
    }

    public ICommand ValidateAndCreateOperationCommand { get; }

    public ICommand UpdateCommand { get; }

    public Money Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    public string Comment
    {
        get => _comment;
        set => SetProperty(ref _comment, value);
    }

    public OperationCategory Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public DateTimeOffset Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public TimeSpan Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    public bool IsValid => Amount > 0 && _category is not null;

    private void Update()
    {
        AvailableOperationCategories = new(_dataProvider.GetCategories());
        Category = AvailableOperationCategories.FirstOrDefault(c => c.OperationCategoryId == (Category?.OperationCategoryId ?? 0));
    }

    private void CreateOperation()
    {
        if (!IsValid)
        {
            return;
        }

        _dataProvider.Add(new Operation(_amount, _category, _comment, _date.DateTime + _time));
    }
}

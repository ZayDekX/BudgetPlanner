using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BudgetPlanner.Data;
using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;
using BudgetPlanner.Utils.Commands;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels.Implementation;

public class OperationCreatorViewModel : ObservableObject, IOperationCreatorViewModel
{
    public OperationCreatorViewModel(IOperationProvider opetaionProvider, ICategoryProvider categoryProvider)
    {
        ValidateAndCreateOperationCommand = new DispatcherCommand(CreateOperation);
        UpdateCommand = new DispatcherCommand(Update);

        Date = DateTime.Today;
        Time = DateTime.Now - DateTime.Today;

        _operationProvider = opetaionProvider;
        _categoryProvider = categoryProvider;
    }

    private Money _amount;
    private string _comment;
    private ICategoryViewModel _category;
    private DateTimeOffset _date;
    private TimeSpan _time;
    private ObservableCollection<ICategoryViewModel> _availableCategories = new();

    private readonly IOperationProvider _operationProvider;
    private readonly ICategoryProvider _categoryProvider;

    public ObservableCollection<ICategoryViewModel> AvailableCategories
    {
        get => _availableCategories;
        set => SetProperty(ref _availableCategories, value);
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

    public ICategoryViewModel Category
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
        AvailableCategories = new(_categoryProvider.GetCategories().Select(c => new CategoryViewModel(c)));
    }

    private void CreateOperation()
    {
        if (!IsValid)
        {
            return;
        }

        _operationProvider.Add(new Operation(_amount, _category.AsModel(), _comment, _date.DateTime + _time));
    }
}

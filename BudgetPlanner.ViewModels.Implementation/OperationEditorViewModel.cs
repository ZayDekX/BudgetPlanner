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

public class OperationEditorViewModel : ObservableObject, IOperationEditorViewModel
{
    public OperationEditorViewModel(IOperationProvider dataProvider, ICategoryProvider categoryProvider)
    {
        _operationProvider = dataProvider;
        _categoryProvider = categoryProvider;

        ValidateAndUpdateOperationCommand = new DispatcherCommand(ValidateAndUpdateOperation);
        UpdateCommand = new DispatcherCommand(Update);
    }

    public void Init(IOperationViewModel viewModel)
    {
        _operationId = viewModel.Id;
        Amount = viewModel.Amount;
        Comment = viewModel.Comment;
        Category = viewModel.Category;
        Date = viewModel.DateTime.Date;
        Time = viewModel.DateTime.TimeOfDay;
    }

    private void Update()
    {
        AvailableCategories = new(_categoryProvider.GetCategories().Select(c => new CategoryViewModel(c)));
        Category = AvailableCategories.First(c => c.Id == (Category?.Id ?? 0));
    }

    private void ValidateAndUpdateOperation()
    {
        if (!IsValid)
        {
            return;
        }

        UpdateOperation();
    }

    private int _operationId;

    private Money _amount;
    private string _comment;
    private ICategoryViewModel _category;
    private DateTimeOffset _date;
    private TimeSpan _time;
    private ObservableCollection<ICategoryViewModel> _availableOperationCategories = new();
    
    private readonly IOperationProvider _operationProvider;
    private readonly ICategoryProvider _categoryProvider;

    public ICommand UpdateCommand { get; }

    public ObservableCollection<ICategoryViewModel> AvailableCategories
    {
        get => _availableOperationCategories;
        set => SetProperty(ref _availableOperationCategories, value);
    }

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

    public ICommand ValidateAndUpdateOperationCommand { get; }

    public bool IsValid => Amount > 0 && _category is not null;

    private void UpdateOperation()
    {
        if (!IsValid)
        {
            return;
        }

        _operationProvider.Update(new Operation(_amount, _category.AsModel(), _comment, _date.DateTime + _time) { OperationId = _operationId });
    }
}

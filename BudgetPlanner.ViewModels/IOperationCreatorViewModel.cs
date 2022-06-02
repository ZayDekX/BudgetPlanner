using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using BudgetPlanner.Data;

namespace BudgetPlanner.ViewModels;
public interface IOperationCreatorViewModel
{
    Money Amount { get; set; }

    ObservableCollection<ICategoryViewModel> AvailableCategories { get; set; }

    ICategoryViewModel Category { get; set; }

    string Comment { get; set; }

    DateTimeOffset Date { get; set; }

    bool IsValid { get; }

    TimeSpan Time { get; set; }

    ICommand UpdateCommand { get; }

    ICommand ValidateAndCreateOperationCommand { get; }
}
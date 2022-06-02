using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BudgetPlanner.ViewModels;

public interface IHistoryViewModel
{
    ObservableCollection<IGrouping<DateTime, IOperationViewModel>> AvailableOperations { get; set; }

    IOperationViewModel SelectedOperation { get; set; }

    ICommand UpdateCommand { get; }

    void DeleteSelectedOperation();
}
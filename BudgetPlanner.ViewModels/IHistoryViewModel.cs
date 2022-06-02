using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace BudgetPlanner.ViewModels;

public interface IHistoryViewModel : INotifyPropertyChanged
{
    string PeriodSelectorText { get; set; }

    ObservableCollection<IGrouping<DateTime, IOperationViewModel>> AvailableOperations { get; set; }

    IOperationViewModel SelectedOperation { get; set; }

    ICommand UpdateCommand { get; }

    ICommand UpdateDateRangeCommand { get; }

    ICommand DeleteSelectedOperationCommand { get; }

    ICommand DeselectRangeCommand { get; }
}
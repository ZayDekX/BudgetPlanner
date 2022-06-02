using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

namespace BudgetPlanner.ViewModels;

public interface IOverviewViewModel : INotifyPropertyChanged
{
    Money Available { get; set; }

    IEnumerable<OverviewPeriod> AvailablePeriods { get; }

    Money Incomes { get; set; }

    Money Outcomes { get; set; }

    OverviewPeriod SelectedPeriod { get; set; }

    string SelectedPeriodRange { get; set; }

    ObservableCollection<CategoryStats> Stats { get; set; }

    Money TotalSpent { get; set; }

    ICommand UpdateCommand { get; }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;

using BudgetPlanner.Models;
using BudgetPlanner.Data;
using System.Windows.Input;

namespace BudgetPlanner.ViewModels;

public interface IOverviewViewModel
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
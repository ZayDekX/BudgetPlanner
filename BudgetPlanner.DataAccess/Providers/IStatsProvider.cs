using System;
using System.Collections.Generic;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Providers;
public interface IStatsProvider
{
    IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate);
    
    Money GetIncomes(DateTime startDate);

    Money GetOutcomes(DateTime startDate);

    Money GetAvailable();
}

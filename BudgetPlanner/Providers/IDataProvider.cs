using System;
using System.Collections.Generic;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

namespace BudgetPlanner.Providers
{
    public interface IDataProvider
    {
        IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate);

        IEnumerable<Operation> GetOperations();
    }
}

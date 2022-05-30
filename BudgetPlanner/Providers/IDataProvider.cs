using System;
using System.Collections.Generic;

using BudgetPlanner.Data;
using BudgetPlanner.Models;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Providers
{
    public interface IDataProvider
    {
        IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate);

        IEnumerable<Operation> GetOperations();

        void DeleteOperation(OperationViewModel selectedOperation);
    }
}

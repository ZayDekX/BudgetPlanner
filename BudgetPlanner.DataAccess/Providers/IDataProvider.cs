using System;
using System.Collections.Generic;

using BudgetPlanner.Data;
using BudgetPlanner.Models;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.DataAccess.Providers;

public interface IDataProvider
{
    IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate);

    IEnumerable<Operation> GetOperations();

    void DeleteOperation(OperationViewModel selectedOperation);

    void Add(Operation operation);

    void Update(Operation operation);

    IEnumerable<OperationCategory> GetCategories();
}

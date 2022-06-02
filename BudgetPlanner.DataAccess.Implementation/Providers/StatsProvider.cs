using BudgetPlanner.Data;
using System.Collections.Generic;
using System;

using BudgetPlanner.DataAccess.Providers;
using System.Linq;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Implementation.Providers;

public class StatsProvider : IStatsProvider
{
    public StatsProvider(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    private readonly IDataSource _dataSource;
 
    public IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate)
    {
        var source = _dataSource;

        var categories = source.Categories.AsEnumerable();
        var operations = FilterOperations(source.Operations.AsEnumerable(), startDate, OperationType.Outcome).ToList();

        var stats = categories
            .Take(Math.Min(categories.Count(), 5))
            .Select(c => new CategoryStats(c, Sum(operations.Where(o => o.Category.CategoryId == c.CategoryId))))
            .Where(s => s.Spent > 0);

        return stats;
    }

    public Money GetIncomes(DateTime startDate)
    {
        var operations = FilterOperations(_dataSource.Operations.AsEnumerable(), startDate, OperationType.Income);
        return Sum(operations);
    }

    public Money GetOutcomes(DateTime startDate)
    {
        var operations = FilterOperations(_dataSource.Operations.AsEnumerable(), startDate, OperationType.Outcome);
        return Sum(operations);
    }

    private Money Sum(IEnumerable<Operation> operations)
    {
        return new Money(operations.Select(o => o.Amount.Amount * (o.Category.OperationType == OperationType.Outcome ? -1 : 1)).Sum());
    }

    private IEnumerable<Operation> FilterOperations(IEnumerable<Operation> operations, DateTime startDate, OperationType operationType)
    {
        return operations.Where(o => o.Category.OperationType == operationType && o.DateTime > startDate);
    }

    public Money GetAvailable()
    {
        return new(_dataSource.Operations.AsEnumerable().Select(o => o.Amount.Amount * (o.Category.OperationType == OperationType.Outcome ? -1 : 1)).Sum());
    }
}

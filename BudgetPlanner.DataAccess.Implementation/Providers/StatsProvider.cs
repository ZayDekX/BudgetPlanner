using System;
using System.Collections.Generic;
using System.Linq;

using BudgetPlanner.Data;
using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Implementation.Providers;

public class StatsProvider : IStatsProvider
{
    public StatsProvider(IDataSourceProvider dataSource)
    {
        _dataSourceProvider = dataSource;
    }

    private readonly IDataSourceProvider _dataSourceProvider;

    public IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate)
    {
        var source = _dataSourceProvider.GetInstance();

        var categories = source.Categories.ToList();
        var operations = FilterOperations(source.Operations.AsEnumerable(), startDate, OperationType.Outcome).ToList();

        return categories
            .Take(Math.Min(categories.Count(), 5))
            .Select(c => new CategoryStats(c, new(operations.Where(o => o.Category.CategoryId == c.CategoryId).Select(o => o.Amount.Amount).Sum())))
            .Where(s => s.Spent > 0);
    }

    public Money GetIncomes(DateTime startDate)
    {
        var source = _dataSourceProvider.GetInstance();

        var operations = FilterOperations(source.Operations.AsEnumerable(), startDate, OperationType.Income);
        return Sum(operations);
    }

    public Money GetOutcomes(DateTime startDate)
    {
        var source = _dataSourceProvider.GetInstance();

        var operations = FilterOperations(source.Operations.AsEnumerable(), startDate, OperationType.Outcome);
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
        var source = _dataSourceProvider.GetInstance();

        return new(source.Operations.AsEnumerable().Select(o => o.Amount.Amount * (o.Category.OperationType == OperationType.Outcome ? -1 : 1)).Sum());
    }
}

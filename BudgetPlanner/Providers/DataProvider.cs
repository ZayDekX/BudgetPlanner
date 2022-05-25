using System;
using System.Collections.Generic;
using System.Linq;

using BudgetPlanner.Contexts;
using BudgetPlanner.Data;
using BudgetPlanner.Models;

namespace BudgetPlanner.Providers
{
    internal class DataProvider : IDataProvider
    {
        public static IDataProvider Instance { get; } = new DataProvider();

        public IEnumerable<CategoryStats> GetCategoryStats(DateTime startDate)
        {
            var source = GetDataSource();

            var categories = source.Categories.ToList();
            var operations = source.Operations.Where(o => o.DateTime > startDate).ToList();

            var stats = categories
                .Where(c => c.OperationType is OperationType.Outcome)
                .Take(Math.Min(categories.Count(), 5))
                .Select(c => new CategoryStats(c, new Money(operations.Where(o => o.Category.OperationCategoryId == c.OperationCategoryId).Sum(o => o.Amount), Settings.CurrencyMarker)))
                .Where(s => s.Spent > 0);

            return stats;
        }

        public IEnumerable<Operation> GetOperations()
        {
            var source = GetDataSource();

            var operations = source.Operations.ToList();

            return operations;
        }

        public IEnumerable<Operation> GetOperations(int max)
        {
            var source = GetDataSource();

            var operations = source.Operations.ToList();

            if (max < 0)
            {
                return operations;
            }

            return operations.Take(Math.Min(operations.Count, max));
        }

        private BudgetPlannerContext GetDataSource()
        {
            return new BudgetPlannerContext();
        }
    }
}

using System.Collections.Generic;

using BudgetPlanner.Data;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Providers
{
    internal class StatsProvider : IStatsProvider
    {
        public static IStatsProvider Default { get; } = new StatsProvider();

        public IReadOnlyList<CategoryStats> GetCategoryStatsFor(OverviewPeriod period, OverviewViewModel viewModel)
        {
            return new List<CategoryStats>();
        }
    }
}

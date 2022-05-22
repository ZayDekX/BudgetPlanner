using System.Collections.Generic;

using BudgetPlanner.Data;

namespace BudgetPlanner.Providers
{
    internal class StatsProvider : IStatsProvider
    {
        public static IStatsProvider Default { get; } = new StatsProvider();

        public IReadOnlyList<CategoryStats> GetStats(OverviewPeriod period)
        {
            return new List<CategoryStats>();
        }
    }
}

using System.Collections.Generic;

using BudgetPlanner.Data;

namespace BudgetPlanner.Providers
{
    public interface IStatsProvider
    {
        IReadOnlyList<CategoryStats> GetStats(OverviewPeriod period);
    }
}

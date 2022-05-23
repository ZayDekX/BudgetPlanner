using System.Collections.Generic;

using BudgetPlanner.Data;
using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Providers
{
    public interface IStatsProvider
    {
        IReadOnlyList<CategoryStats> GetCategoryStatsFor(OverviewPeriod period, OverviewViewModel viewModel);
    }
}

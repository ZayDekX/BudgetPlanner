using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Data
{
    public sealed class CategoryStats
    {
        public CategoryStats(OverviewViewModel sourceViewModel, OperationCategory categoryInfo, float totalSpent)
        {
            SourceViewModel = sourceViewModel;
            CategoryInfo = categoryInfo;
            TotalSpent = totalSpent;
        }

        public OverviewViewModel SourceViewModel { get; }

        public OperationCategory CategoryInfo { get; }

        public float TotalSpent { get; }
    }
}

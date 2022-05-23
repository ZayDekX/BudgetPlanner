using BudgetPlanner.ViewModels;

namespace BudgetPlanner.Data
{
    public sealed class CategoryStats
    {
        public CategoryStats(OverviewViewModel sourceViewModel, OperationCategory categoryInfo, Money spent)
        {
            SourceViewModel = sourceViewModel;
            CategoryInfo = categoryInfo;
            Spent = spent;
        }

        public OverviewViewModel SourceViewModel { get; }

        public OperationCategory CategoryInfo { get; }

        public Money Spent { get; }
    }
}

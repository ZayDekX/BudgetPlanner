using BudgetPlanner.ViewModels;

using Windows.UI;

namespace BudgetPlanner.Data
{
    public sealed class CategoryStats
    {
        public CategoryStats(OverviewViewModel sourceViewModel, OperationCategory categoryInfo, Money spent, Color color)
        {
            SourceViewModel = sourceViewModel;
            CategoryInfo = categoryInfo;
            Spent = spent;
            Color = color;
        }

        public OverviewViewModel SourceViewModel { get; }

        public OperationCategory CategoryInfo { get; }

        public Money Spent { get; }

        public Color Color { get; }
    }
}

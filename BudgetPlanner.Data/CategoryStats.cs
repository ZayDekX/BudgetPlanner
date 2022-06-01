namespace BudgetPlanner.Data;

public sealed class CategoryStats
{
    public CategoryStats(OperationCategory categoryInfo, Money spent)
    {
        CategoryInfo = categoryInfo;
        Spent = spent;
    }

    public OperationCategory CategoryInfo { get; }

    public Money Spent { get; }
}

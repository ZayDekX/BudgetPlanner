using BudgetPlanner.Data;

namespace BudgetPlanner.Models;

public sealed class CategoryStats
{
    public CategoryStats(Category categoryInfo, Money spent)
    {
        CategoryInfo = categoryInfo;
        Spent = spent;
    }

    public Category CategoryInfo { get; }

    public Money Spent { get; }
}

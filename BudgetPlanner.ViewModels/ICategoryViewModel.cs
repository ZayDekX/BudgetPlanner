using BudgetPlanner.Models;

namespace BudgetPlanner.ViewModels;

public interface ICategoryViewModel
{
    int Id { get; }

    string Name { get; }

    Category AsModel();
}
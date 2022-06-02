using System.Collections.Generic;

using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Providers;
public interface ICategoryProvider
{
    IEnumerable<Category> GetCategories();
}

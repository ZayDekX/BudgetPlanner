using System.Collections.Generic;

using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Implementation.Providers;

public class CategoryProvider : ICategoryProvider
{
    private readonly IDataSource _dataSource;

    public CategoryProvider(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _dataSource.Categories;
    }
}

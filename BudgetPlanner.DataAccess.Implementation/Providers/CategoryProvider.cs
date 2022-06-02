using System.Collections.Generic;

using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Implementation.Providers;

public class CategoryProvider : ICategoryProvider
{
    private readonly IDataSourceProvider _dataSourceProvider;

    public CategoryProvider(IDataSourceProvider dataSource)
    {
        _dataSourceProvider = dataSource;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _dataSourceProvider.GetInstance().Categories;
    }
}

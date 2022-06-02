using BudgetPlanner.Models;

namespace BudgetPlanner.ViewModels.Implementation;

internal class CategoryViewModel : ICategoryViewModel
{
    private readonly Category _model;

    public CategoryViewModel(Category model)
    {
        _model = model;
    }

    public int Id => _model.CategoryId;

    public string Name => _model.Name;

    public Category AsModel()
    {
        return _model;
    }
}

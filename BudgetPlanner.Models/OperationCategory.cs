using Windows.UI;

namespace BudgetPlanner.Data;

public sealed class OperationCategory
{
    public OperationCategory()
    {

    }

    public OperationCategory(string name, OperationType operationType, Color color)
    {
        Name = name;
        OperationType = operationType;
        Color = color;
    }

    public int OperationCategoryId { get; set; }

    public string Name { get; set; }

    public OperationType OperationType { get; set; }

    public Color Color { get; set; }

    public override string ToString()
    {
        return Name;
    }
}

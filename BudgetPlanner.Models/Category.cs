using System.Drawing;

using BudgetPlanner.Data;

namespace BudgetPlanner.Models;

public sealed class Category
{
    public Category()
    {

    }

    public Category(string name, OperationType operationType, Color color)
    {
        Name = name;
        OperationType = operationType;
        Color = color;
    }

    public int CategoryId { get; set; }

    public string Name { get; set; }

    public OperationType OperationType { get; set; }

    public Color Color { get; set; }

    public override string ToString()
    {
        return Name;
    }
}

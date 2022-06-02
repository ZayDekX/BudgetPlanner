using System;

using BudgetPlanner.Data;

namespace BudgetPlanner.Models;

public sealed class Operation
{
    public Operation()
    {

    }

    public Operation(Money money, Category category, string comment, DateTime dateTime)
    {
        Amount = money;
        Category = category;
        Comment = comment;
        DateTime = dateTime;
    }

    public int OperationId { get; set; }

    public Money Amount { get; set; }

    public Category Category { get; set; }

    public DateTime DateTime { get; set; }

    public string Comment { get; set; }
}

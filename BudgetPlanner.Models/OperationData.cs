using System;

using BudgetPlanner.Data;

namespace BudgetPlanner.Models;

public sealed class OperationData
{
    public OperationData()
    {

    }

    public OperationData(Money money, OperationCategory category, string comment, DateTime dateTime)
    {
        Amount = money;
        Category = category;
        Comment = comment;
        DateTime = dateTime;
    }

    public int OperationId { get; set; }

    public Money Amount { get; set; }

    public OperationCategory Category { get; set; }

    public DateTime DateTime { get; set; }

    public string Comment { get; set; }
}

using System;
using System.Drawing;

using BudgetPlanner.Data;

namespace BudgetPlanner.ViewModels;

public interface IOperationViewModel
{
    Money Amount { get; }

    Color AmountForeground { get; }

    string AmountSign { get; }

    string Comment { get; }

    DateTime DateTime { get; }

    int Id { get; }

    ICategoryViewModel Category { get; }
}
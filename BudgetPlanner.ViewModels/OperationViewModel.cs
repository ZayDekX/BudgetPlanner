using System;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

using Windows.UI;

namespace BudgetPlanner.ViewModels;

public class OperationViewModel
{
    private readonly Operation _model;

    private static readonly Color _greenColor = Color.FromArgb(0xff, 0x2a, 0xcc, 0x55);
    private static readonly Color _redColor = Color.FromArgb(0xff, 0xcc, 0x2a, 0x2a);

    public OperationViewModel(Operation model)
    {
        _model = model;
    }

    public Money Amount => _model.Amount;

    public string Comment => _model.Comment;

    public string CategoryName => _model.Category.Name;

    public DateTime DateTime => _model.DateTime;

    public Color AmountForeground => _model.Category.OperationType is OperationType.Income ? _greenColor : _redColor;

    public string AmountSign => _model.Category.OperationType is OperationType.Income ? "+" : "-";

    public static explicit operator Operation(OperationViewModel viewModel)
    {
        return viewModel._model;
    }
}

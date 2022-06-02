using System;
using System.Drawing;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

namespace BudgetPlanner.ViewModels.Implementation;

public class OperationViewModel : IOperationViewModel
{
    private readonly Operation _model;

    private static readonly Color _greenColor = Color.FromArgb(0xff, 0x2a, 0xcc, 0x55);
    private static readonly Color _redColor = Color.FromArgb(0xff, 0xcc, 0x2a, 0x2a);

    public OperationViewModel(Operation model)
    {
        _model = model;
        Category = new CategoryViewModel(_model.Category);
    }

    public Money Amount => _model.Amount;

    public string Comment => _model.Comment;

    public ICategoryViewModel Category { get; }

    public DateTime DateTime => _model.DateTime;

    public Color AmountForeground => _model.Category.OperationType is OperationType.Income ? _greenColor : _redColor;

    public string AmountSign => _model.Category.OperationType is OperationType.Income ? "+" : "-";

    public int Id => _model.OperationId;

    public Operation AsModel()
    {
        return _model;
    }
}

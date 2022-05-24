using BudgetPlanner.Data;
using BudgetPlanner.Models;

using Windows.UI;
using Windows.UI.Xaml.Media;

namespace BudgetPlanner.ViewModels
{
    public class OperationViewModel
    {
        private readonly Operation _model;

        public OperationViewModel(Operation model)
        {
            _model = model;
        }

        public Money Amount => _model.Amount;

        public string Comment => _model.Comment;

        public string CategoryName => _model.Category.Name;

        public Brush AmountForeground => new SolidColorBrush(_model.Category.OperationType is OperationType.Income ? Colors.Green : Colors.Red);
    }
}

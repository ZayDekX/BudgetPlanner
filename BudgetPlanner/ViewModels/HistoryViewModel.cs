using System.Collections.ObjectModel;
using System.Linq;

using BudgetPlanner.Contexts;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        private OperationViewModel _selectedOperation;

        public ObservableCollection<OperationViewModel> AvailableOperations { get; } = new();

        public OperationViewModel SelectedOperation
        {
            get => _selectedOperation;
            set => SetProperty(ref _selectedOperation, value);
        }

        internal void Update()
        {
            AvailableOperations.Clear();

            var context = new BudgetPlannerContext();

            var operations = context.Operations.OrderByDescending(o => o.DateTime).Select(o => new OperationViewModel(o)).ToList();

            foreach (var operation in operations)
            {
                AvailableOperations.Add(operation);
            }
        }
    }
}

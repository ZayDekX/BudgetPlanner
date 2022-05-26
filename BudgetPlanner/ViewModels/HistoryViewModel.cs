using System.Collections.ObjectModel;
using System.Linq;

using BudgetPlanner.Contexts;
using BudgetPlanner.Data;
using BudgetPlanner.Providers;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        public HistoryViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private OperationViewModel _selectedOperation;
        private ObservableCollection<OperationViewModel> _availableOperations = new();
        private readonly IDataProvider _dataProvider;

        public ObservableCollection<OperationViewModel> AvailableOperations
        {
            get => _availableOperations;
            set => SetProperty(ref _availableOperations, value);
        }

        public OperationViewModel SelectedOperation
        {
            get => _selectedOperation;
            set => SetProperty(ref _selectedOperation, value);
        }

        internal void Update()
        {
            var operations =
                _dataProvider
                    .GetOperations(Settings.MaxOperations)
                    .OrderByDescending(o => o.DateTime)
                    .Select(o => new OperationViewModel(o));

            AvailableOperations = new(operations);
        }
    }
}

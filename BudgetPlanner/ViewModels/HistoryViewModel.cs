using System.Collections.ObjectModel;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        private OperationViewModel _selectedOperation;

        public static HistoryViewModel Instance { get; } = new();

        public ObservableCollection<OperationViewModel> AvailableOperations { get; } = new();

        public OperationViewModel SelectedOperation
        {
            get => _selectedOperation;
            set => SetProperty(ref _selectedOperation, value);
        }
    }
}

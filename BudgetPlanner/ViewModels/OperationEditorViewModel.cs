using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BudgetPlanner.Commands;
using BudgetPlanner.Data;
using BudgetPlanner.Models;
using BudgetPlanner.Providers;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels
{
    public class OperationEditorViewModel : ObservableObject
    {
        public OperationEditorViewModel(IDataProvider dataProvider, Operation seed)
        {
            _dataProvider = dataProvider;

            _operationId = seed.OperationId;
            Amount = seed.Amount;
            Comment = seed.Comment;
            Category = seed.Category;
            Date = seed.DateTime.Date;
            Time = seed.DateTime.TimeOfDay;

            ValidateAndUpdateOperationCommand = new DispatcherCommand(ValidateAndUpdateOperation);
            UpdateCommand = new DispatcherCommand(Update);
        }

        private void Update()
        {
            AvailableOperationCategories = new(_dataProvider.GetCategories());
            Category = AvailableOperationCategories.First(c => c.OperationCategoryId == Category.OperationCategoryId);
        }

        private void ValidateAndUpdateOperation()
        {
            if (!IsValid)
            {
                return;
            }

            UpdateOperation();
        }

        private readonly int _operationId;

        private Money _amount;
        private string _comment;
        private OperationCategory _category;
        private DateTimeOffset _date;
        private TimeSpan _time;
        private ObservableCollection<OperationCategory> _availableOperationCategories = new();
        private readonly IDataProvider _dataProvider;

        public ICommand UpdateCommand { get; }

        public ObservableCollection<OperationCategory> AvailableOperationCategories
        {
            get => _availableOperationCategories;
            set => SetProperty(ref _availableOperationCategories, value);
        }

        public Money Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public OperationCategory Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public DateTimeOffset Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public TimeSpan Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public ICommand ValidateAndUpdateOperationCommand { get; }

        public bool IsValid => Amount > 0 && _category is not null;

        private void UpdateOperation()
        {
            if (!IsValid)
            {
                return;
            }

            _dataProvider.Update(new Operation(_amount, _category, _comment, _date.DateTime + _time) { OperationId = _operationId });
        }
    }
}

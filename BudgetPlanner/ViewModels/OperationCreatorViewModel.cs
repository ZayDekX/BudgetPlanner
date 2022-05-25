using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using BudgetPlanner.Contexts;
using BudgetPlanner.Data;
using BudgetPlanner.Models;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.ViewModels
{
    internal class OperationCreatorViewModel : ObservableObject
    {
        public OperationCreatorViewModel()
        {
            ValidateAndCreateOperationCommand = new RelayCommand(CreateOperation);
            Date = DateTime.Today;
            Time = DateTime.Now - DateTime.Today;
        }

        private Money _amount;
        private string _comment;
        private OperationType _operationType;
        private OperationCategory _category;
        private DateTimeOffset _date;
        private TimeSpan _time;

        public IEnumerable<OperationType> AvailableOperationTypes { get; } = (IEnumerable<OperationType>)Enum.GetValues(typeof(OperationType));

        public ObservableCollection<OperationCategory> AvailableOperationCategories { get; } = new();

        public ICommand ValidateAndCreateOperationCommand { get; }

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

        public int OperationType
        {
            get => (int)_operationType;
            set => SetProperty(ref _operationType, (OperationType)value);
        }

        public bool IsValid => Amount > 0 && _category is not null;

        internal void Update()
        {
            var context = new BudgetPlannerContext();
            AvailableOperationCategories.Clear();

            foreach (var category in context.Categories)
            {
                AvailableOperationCategories.Add(category);
            }
        }

        private void CreateOperation()
        {
            if (!IsValid)
            {
                return;
            }

            var context = new BudgetPlannerContext();
            _ = context.Operations.Add(new Operation(_amount, _category, _comment, _date.DateTime + _time));
            _ = context.Attach(_category);
            _ = context.SaveChanges();
        }

        public void ValidateAmount(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewText))
            {
                _amount = Money.Zero(Settings.CurrencyMarker);
                return;
            }

            if (float.TryParse(args.NewText, out _))
            {
                return;
            }

            args.Cancel = true;
        }
    }
}

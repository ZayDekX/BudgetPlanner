using System;
using System.Collections.Generic;
using System.Windows.Input;

using BudgetPlanner.Data;

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
        private DateTime _date;
        private TimeSpan _time;

        public IEnumerable<OperationType> AvailableOperationTypes { get; } = (IEnumerable<OperationType>)Enum.GetValues(typeof(OperationType));

        public IEnumerable<OperationCategory> AvailableOperationCategories { get; } = Settings.Instance.OperationCategories;

        public ICommand ValidateAndCreateOperationCommand { get; }

        private bool ValidateForm()
        {
            return _category is not null;
        }

        private void CreateOperation()
        {
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

        public DateTime Date
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

        public void ValidateAmount(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewText))
            {
                _amount = Money.Zero(Settings.Instance.CurrencyMarker);
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

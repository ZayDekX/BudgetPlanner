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
            ValidateAndCreateOperationCommand = new RelayCommand(CreateOperation, ValidateForm);
        }

        private float _amount;
        private string _comment;
        private OperationType _operationType;
        private OperationCategory _category;

        public IEnumerable<OperationType> AvailableOperationTypes { get; } = (IEnumerable<OperationType>)Enum.GetValues(typeof(OperationType));

        public IEnumerable<OperationCategory> AvailableOperationCategories { get; } = Array.Empty<OperationCategory>();

        public ICommand ValidateAndCreateOperationCommand { get; }

        private bool ValidateForm()
        {
            return !float.IsInfinity(_amount) && !float.IsNaN(_amount) && _category is not null;
        }

        private void CreateOperation()
        {

        }

        public float Amount
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

        public int OperationType
        {
            get => (int)_operationType;
            set => SetProperty(ref _operationType, (OperationType)value);
        }

        public void ValidateAmount(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewText))
            {
                _amount = 0;
                return;
            }

            if (float.TryParse(args.NewText, out var newValue))
            {
                return;
            }

            args.Cancel = true;
        }
    }
}

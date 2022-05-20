﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BudgetPlanner.Models;

using Windows.UI;
using Windows.UI.Xaml.Media;

namespace BudgetPlanner.ViewModels
{
    public class OperationViewModel
    {
        private readonly OperationModel _model;

        public OperationViewModel(OperationModel model)
        {
            _model = model;
        }

        public float Amount => _model.Amount;
        
        public string Comment => _model.Comment;

        public string CategoryName => _model.Category.Name;

        public Brush AmountForeground => new SolidColorBrush(_model.OperationType is Data.OperationType.Income ? Colors.Green : Colors.Red);
    }
}

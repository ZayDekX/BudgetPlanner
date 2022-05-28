using System;

using BudgetPlanner.Data;

namespace BudgetPlanner.Models
{
    public sealed class OperationModel
    {
        public OperationModel(float amount, OperationType operationType, OperationCategory category, string comment, DateTime dateTime)
        {
            Amount = amount;
            OperationType = operationType;
            Category = category;
            Comment = comment;
            DateTime = dateTime;
        }

        public float Amount { get; }

        public DateTime DateTime { get; }

        public OperationType OperationType { get; }

        public OperationCategory Category { get; }

        public string Comment { get; }
    }
}

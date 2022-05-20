using BudgetPlanner.Data;

namespace BudgetPlanner.Models
{
    public sealed class OperationModel
    {
        public OperationModel(float amount, OperationType operationType, OperationCategory category, string comment)
        {
            Amount = amount;
            OperationType = operationType;
            Category = category;
            Comment = comment;
        }

        public float Amount { get; }

        public OperationType OperationType { get; }

        public OperationCategory Category { get; }

        public bool IsIncome => OperationType is OperationType.Income;

        public bool IsOutcome => OperationType is OperationType.Outcome;

        public string Comment { get; }
    }
}

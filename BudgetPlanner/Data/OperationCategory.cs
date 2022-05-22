namespace BudgetPlanner.Data
{
    public sealed class OperationCategory
    {
        public OperationCategory(string name, OperationType operationType)
        {
            Name = name;
            DefaultOperationType = operationType;
        }

        public string Name { get; }

        public OperationType DefaultOperationType { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}

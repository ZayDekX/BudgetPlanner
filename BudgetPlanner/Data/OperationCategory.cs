namespace BudgetPlanner.Data
{
    public sealed class OperationCategory
    {
        public OperationCategory(string name, OperationType operationType)
        {
            Name = name;
            OperationType = operationType;
        }

        public string Name { get; }
        
        public OperationType OperationType { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}

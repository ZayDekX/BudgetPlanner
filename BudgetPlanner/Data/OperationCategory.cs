namespace BudgetPlanner.Data
{
    public sealed class OperationCategory
    {
        public OperationCategory()
        {

        }

        public OperationCategory(string name, OperationType operationType)
        {
            Name = name;
            OperationType = operationType;
        }

        public int OperationCategoryId { get; set; }

        public string Name { get; set; }

        public OperationType OperationType { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

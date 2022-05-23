using System.Collections.Generic;

namespace BudgetPlanner.Data
{
    internal class Settings
    {
        public static Settings Instance { get; } = new Settings()
        {
            CurrencyMarker = "$",
            OperationCategories = new List<OperationCategory>()
            {
                new("Shopping", OperationType.Outcome),
                new("Food", OperationType.Outcome),
                new("Housing", OperationType.Outcome),
                new("Salary", OperationType.Income),
            }
        };

        public string CurrencyMarker { get; set; }

        public IEnumerable<OperationCategory> OperationCategories { get; internal set; }
    }
}

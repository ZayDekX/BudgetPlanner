using System.Collections.Generic;

using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Providers;
public interface IOperationProvider
{
    IEnumerable<Operation> GetOperations();

    void Delete(int operationId);

    void Add(Operation operation);

    void Update(Operation operation);
}

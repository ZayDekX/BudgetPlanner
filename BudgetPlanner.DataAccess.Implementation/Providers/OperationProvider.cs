using System.Collections.Generic;
using System.Linq;

using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Implementation.Providers;

public class OperationProvider : IOperationProvider
{
    private readonly IDataSource _dataSource;

    public OperationProvider(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Operation> GetOperations()
    {
        var source = _dataSource;

        return source.Operations;
    }

    public void Delete(int operationId)
    {
        var operation = new Operation() { OperationId = operationId };
        
        _dataSource.Operations.Attach(operation);
        _dataSource.Operations.Remove(operation);

        _dataSource.SaveChanges();
    }

    public void Add(Operation operation)
    {
        _dataSource.Operations.Add(operation);
        _dataSource.Attach(operation.Category);
        _dataSource.SaveChanges();
    }

    public void Update(Operation operation)
    {
        _dataSource.Operations.Update(operation);
        _dataSource.SaveChanges();
    }
}

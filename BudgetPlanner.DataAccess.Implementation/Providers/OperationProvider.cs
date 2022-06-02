using System.Collections.Generic;

using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;

namespace BudgetPlanner.DataAccess.Implementation.Providers;

public class OperationProvider : IOperationProvider
{
    private readonly IDataSourceProvider _dataSourceProvider;

    public OperationProvider(IDataSourceProvider dataSource)
    {
        _dataSourceProvider = dataSource;
    }

    public IEnumerable<Operation> GetOperations()
    {
        var source = _dataSourceProvider.GetInstance();

        return source.Operations;
    }

    public void Delete(int operationId)
    {
        var operation = new Operation() { OperationId = operationId };

        var source = _dataSourceProvider.GetInstance();

        source.Operations.Attach(operation);
        source.Operations.Remove(operation);

        source.SaveChanges();
    }

    public void Add(Operation operation)
    {
        var source = _dataSourceProvider.GetInstance();

        source.Operations.Add(operation);
        source.Attach(operation.Category);
        source.SaveChanges();
    }

    public void Update(Operation operation)
    {
        var source = _dataSourceProvider.GetInstance();

        source.Operations.Update(operation);
        source.SaveChanges();
    }
}

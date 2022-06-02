namespace BudgetPlanner.DataAccess;

public interface IDataSourceProvider
{
    IDataSource GetInstance();
}

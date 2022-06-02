namespace BudgetPlanner.DataAccess.Implementation;

public class DataSourceProvider : IDataSourceProvider
{
    public IDataSource GetInstance()
    {
        return new BudgetPlannerDataSource();
    }
}

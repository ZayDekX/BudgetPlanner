using BudgetPlanner.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.DataAccess;
public interface IDataSource
{
    DbSet<Operation> Operations { get; }

    DbSet<Category> Categories { get; }

    int SaveChanges();

    void Attach(Category category);
}

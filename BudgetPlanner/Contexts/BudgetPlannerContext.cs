using System.Collections.Generic;
using System.IO;
using System.Linq;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Contexts
{
    internal class BudgetPlannerContext : DbContext
    {
        public BudgetPlannerContext()
        {
            if (!File.Exists(Settings.AppDataFilePath))
            {
                var file = File.Create(Settings.AppDataFilePath);
                file.Close();
            }

            _ = Database.EnsureCreated();

            if (!Categories.Any())
            {
                Categories.AddRange(_defaultOperationCategories);
                _ = SaveChanges();
            }
        }

        private static readonly List<OperationCategory> _defaultOperationCategories = new()
            {
                new("Shopping", OperationType.Outcome),
                new("Food", OperationType.Outcome),
                new("Housing", OperationType.Outcome),
                new("Salary", OperationType.Income),
            };

        public DbSet<Operation> Operations { get; set; }

        public DbSet<OperationCategory> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlite($"Filename={Settings.AppDataFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Operation>(builder =>
                {
                    _ = builder
                        .Property(e => e.Amount)
                        .HasConversion(
                            v => (int)(v.Amount * 100),
                            v => new Money(v, Settings.CurrencyMarker));
                    _ = builder
                        .Property(e => e.Category)
                        .HasConversion(
                            v => v.OperationCategoryId,
                            v => Categories.First(c => c.OperationCategoryId == v));
                });
        }
    }
}

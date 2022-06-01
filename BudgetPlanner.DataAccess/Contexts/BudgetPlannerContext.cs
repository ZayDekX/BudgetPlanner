using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using BudgetPlanner.Data;
using BudgetPlanner.Models;

using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Contexts;

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
            new("Shopping", OperationType.Outcome, Color.FromArgb(0xFF, 0x5A, 0xB1, 0xBB)),
            new("Food", OperationType.Outcome, Color.FromArgb(0xFF, 0xA5, 0xC8, 0x82)),
            new("Housing", OperationType.Outcome, Color.FromArgb(0xFF, 0xF7, 0xDD, 0x72)),
            new("Salary", OperationType.Income, Color.FromArgb(0xFF, 0x98, 0x93, 0xDA)),
        };

    public DbSet<OperationData> Operations { get; set; }

    public DbSet<OperationCategory> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.UseSqlite($"Filename={Settings.AppDataFilePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Operation>(builder =>
        {
            builder
                .Property(e => e.Amount)
                .HasConversion(
                    v => (int)(v.Amount * 100),
                    v => new Money(v, Settings.CurrencyMarker));
            builder
                .Property(e => e.Category)
                .HasConversion(
                    v => v.OperationCategoryId,
                    v => Categories.First(c => c.OperationCategoryId == v));
        });
        modelBuilder.Entity<OperationCategory>()
            .Property(e => e.Color)
            .HasConversion(
                v => (uint)(v.A << 24) | (uint)(v.R << 16) | (uint)(v.G << 8) | v.B,
                v => Color.FromArgb((byte)(v >> 24), (byte)(v >> 16), (byte)(v >> 8), (byte)v));
    }
}

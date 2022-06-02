using BudgetPlanner.DataAccess;
using BudgetPlanner.DataAccess.Implementation;
using BudgetPlanner.DataAccess.Implementation.Providers;
using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.ViewModels;
using BudgetPlanner.ViewModels.Implementation;

using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlanner.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViewModelServices(this IServiceCollection collection)
    {
        return collection
            .AddTransient<IOverviewViewModel, OverviewViewModel>()
            .AddTransient<IHistoryViewModel, HistoryViewModel>()
            .AddTransient<IOperationCreatorViewModel, OperationCreatorViewModel>()
            .AddTransient<IOperationEditorViewModel, OperationEditorViewModel>();
    }

    public static IServiceCollection AddDataAccessServices(this IServiceCollection collection)
    {
        return collection
            .AddTransient<IDataSource, BudgetPlannerDataSource>()
            .AddTransient<IOperationProvider, OperationProvider>()
            .AddTransient<ICategoryProvider, CategoryProvider>()
            .AddTransient<IStatsProvider, StatsProvider>();
    }
}
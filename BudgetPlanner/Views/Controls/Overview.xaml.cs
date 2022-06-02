using BudgetPlanner.ViewModels;
using BudgetPlanner.Views.Pages;

using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace BudgetPlanner.Views.Controls;

public sealed partial class Overview
{
    public IOverviewViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<IOverviewViewModel>();

    public Overview()
    {
        InitializeComponent();
        Loaded += Update;
    }

    private void Update(object sender, object args)
    {
        ViewModel.UpdateCommand.Execute(null);
    }

    private void OpenHistoryClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        App.CurrentShell.Navigate<HistoryPage>();
    }
}

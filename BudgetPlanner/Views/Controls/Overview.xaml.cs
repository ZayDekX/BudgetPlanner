using BudgetPlanner.ViewModels;

using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace BudgetPlanner.Views.Controls;

public sealed partial class Overview
{
    public IOverviewViewModel ViewModel { get; set; }

    public Overview()
    {
        InitializeComponent();
        Loaded += Update;
    }

    private void Update(object sender, object args)
    {
        ViewModel = Ioc.Default.GetRequiredService<IOverviewViewModel>();
        ViewModel.UpdateCommand.Execute(null);
    }
}

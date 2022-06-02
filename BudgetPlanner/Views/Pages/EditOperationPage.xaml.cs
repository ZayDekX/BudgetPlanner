using BudgetPlanner.ViewModels;

using Microsoft.Toolkit.Mvvm.DependencyInjection;

using Windows.UI.Xaml.Navigation;

namespace BudgetPlanner.Views.Pages;

public sealed partial class EditOperationPage
{
    public EditOperationPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is not IOperationViewModel viewModel)
        {
            return;
        }

        View.ViewModel = Ioc.Default.GetRequiredService<IOperationEditorViewModel>();
        View.ViewModel.Init(viewModel);
    }
}

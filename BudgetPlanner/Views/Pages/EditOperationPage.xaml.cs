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

        var editorViewModel = Ioc.Default.GetRequiredService<IOperationEditorViewModel>();
        editorViewModel.Init(viewModel);

        View.DataContext = editorViewModel;
    }
}

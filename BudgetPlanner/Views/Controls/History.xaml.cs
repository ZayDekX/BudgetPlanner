using System;
using System.Threading.Tasks;

using BudgetPlanner.ViewModels;
using BudgetPlanner.Views.Pages;

using Microsoft.Toolkit.Mvvm.DependencyInjection;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls;

public sealed partial class History
{
    private IHistoryViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<IHistoryViewModel>();

    public History()
    {
        InitializeComponent();
        Loaded += Update;
    }

    private void Update(object sender, object args)
    {
        ViewModel.UpdateCommand.Execute(null);
    }

    private async void OnDeleteButtonClick(object sender, RoutedEventArgs e)
    {
        await DisplayDeleteFileDialog();
    }

    private async Task DisplayDeleteFileDialog()
    {
        var deleteFileDialog = new ContentDialog
        {
            Title = "Delete selected operation permanently?",
            Content = "If you delete this operation, you won't be able to recover it. Do you want to delete it?",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel"
        };

        var result = await deleteFileDialog.ShowAsync();

        if (result is ContentDialogResult.Primary)
        {
            ViewModel.DeleteSelectedOperationCommand.Execute(null);
        }
    }

    private void OnEditButtonClick(object sender, RoutedEventArgs e)
    {
        App.CurrentShell.Navigate<EditOperationPage>(ViewModel.SelectedOperation);
    }

    private void OnSelectedDatesChanged(DateTimeOffset start, DateTimeOffset end)
    {
        ViewModel.UpdateDateRangeCommand.Execute((start, end));
    }

    private void OnDatesDeselected()
    {
        ViewModel.DeselectRangeCommand.Execute(null);
    }
}

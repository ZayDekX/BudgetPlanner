using BudgetPlanner.ViewModels;

using Microsoft.Toolkit.Mvvm.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls;

public sealed partial class OperationCreatorView
{
    private IOperationCreatorViewModel ViewModel { get; } = Ioc.Default.GetRequiredService<IOperationCreatorViewModel>();

    public OperationCreatorView()
    {
        InitializeComponent();
        Loaded += Update;
    }

    private void Update(object sender, object args)
    {
        ViewModel.UpdateCommand.Execute(null);
    }

    private void OnCancelClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        App.CurrentShell.GoBack();
    }

    public void ValidateAmount(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
    {
        if (string.IsNullOrEmpty(args.NewText) || float.TryParse(args.NewText, out _))
        {
            return;
        }

        args.Cancel = true;
    }
}

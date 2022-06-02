using BudgetPlanner.ViewModels;

using Microsoft.Toolkit.Mvvm.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls;

public sealed partial class OperationEditor
{
    public IOperationEditorViewModel ViewModel { get; set; }

    public OperationEditor()
    {
        InitializeComponent();
        Loaded += Update;
    }

    private void Update(object sender, object args)
    {
        ViewModel = Ioc.Default.GetRequiredService<IOperationEditorViewModel>();
        ViewModel.UpdateCommand.Execute(null);
    }

    private void ValidateAmount(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
    {
        if (string.IsNullOrEmpty(args.NewText) || float.TryParse(args.NewText, out _))
        {
            return;
        }

        args.Cancel = true;
    }

    private void OnCancelClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        App.CurrentShell.GoBack();
    }
}

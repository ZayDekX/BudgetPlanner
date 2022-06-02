using BudgetPlanner.Data;
using BudgetPlanner.ViewModels;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls;

public sealed partial class OperationEditor
{
    private IOperationEditorViewModel ViewModel => (IOperationEditorViewModel)DataContext;

    public OperationEditor()
    {
        InitializeComponent();
        Loaded += Update;
    }

    private void Update(object sender, object args)
    {
        ViewModel.UpdateCommand.Execute(null);
    }

    private void ValidateAmount(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
    {
        var text = args.NewText.TrimEnd(Settings.CurrencyMarker.ToCharArray());

        if (string.IsNullOrEmpty(text) || float.TryParse(text, out _))
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

using BudgetPlanner.Models;
using BudgetPlanner.Providers;
using BudgetPlanner.ViewModels;

using Windows.UI.Xaml.Navigation;

namespace BudgetPlanner.Views.Pages
{
    public sealed partial class EditOperationPage
    {
        public EditOperationPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is not OperationViewModel viewModel)
            {
                return;
            }

            View.ViewModel = new(DataProvider.Instance, (Operation)viewModel);
        }
    }
}

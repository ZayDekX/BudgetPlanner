using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.UI.Xaml.Controls;

namespace BudgetPlanner.Views
{
    public class PageNavigator : NavigationViewItem
    {
        [Required]
        public Type PageType { get; set; }
    }
}

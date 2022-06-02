using System;

using Microsoft.UI.Xaml.Controls;

namespace BudgetPlanner.Views;

public class PageNavigator : NavigationViewItem
{
    public Type PageType { get; set; }
}

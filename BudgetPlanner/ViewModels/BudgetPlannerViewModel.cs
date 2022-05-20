using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using BudgetPlanner.Views;
using BudgetPlanner.Templates;

using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels
{
    internal class BudgetPlannerViewModel : ObservableObject
    {
        public BudgetPlannerViewModel()
        {
            foreach (var template in _templates)
            {
                var item = new NavigationViewItem() { Content = template.MenuHeader, Icon = template.Icon };
                var view = (UserControl)Activator.CreateInstance(template.ViewType);

                MenuItems.Add(item);
                Views.Add(item, view);
            }

            SelectedView = Views.FirstOrDefault().Value;
        }

        private UserControl _selectedView;

        private readonly List<PageTemplate> _templates = new()
        {
            new PageTemplate("Overview", new SymbolIcon(Symbol.AllApps), typeof(OverviewView)),
            new PageTemplate("History", new SymbolIcon(Symbol.Clock), typeof(HistoryView)),
            new PageTemplate("Add operations", new SymbolIcon(Symbol.Edit), typeof(OperationCreatorView))
        };

        public Dictionary<NavigationViewItem, UserControl> Views { get; } = new();

        public ObservableCollection<NavigationViewItem> MenuItems { get; } = new();

        public UserControl SelectedView
        {
            get => _selectedView;
            private set => SetProperty(ref _selectedView, value);
        }

        public void OnMenuSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not NavigationViewItem selectedItem)
            {
                return;
            }

            SelectedView = Views[selectedItem];
        }
    }
}

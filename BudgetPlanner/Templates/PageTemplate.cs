using System;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Templates
{
    internal class PageTemplate
    {
        public PageTemplate(string menuHeader, IconElement icon, Type viewType)
        {
            MenuHeader = menuHeader;
            Icon = icon;
            ViewType = viewType;
        }

        public string MenuHeader { get; }

        public IconElement Icon { get; }

        public Type ViewType { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BudgetPlanner.Data;
using BudgetPlanner.Providers;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels
{
    public class OverviewViewModel : ObservableObject
    {
        public OverviewViewModel(IStatsProvider statsProvider)
        {
            _statsProvider = statsProvider;
        }

        private OverviewPeriod _selectedPeriod;

        private Money _totalSpent = Money.Zero(Settings.Instance.CurrencyMarker);
        private Money _incomes = Money.Zero(Settings.Instance.CurrencyMarker);
        private Money _outcomes = Money.Zero(Settings.Instance.CurrencyMarker);
        private Money _available = Money.Zero(Settings.Instance.CurrencyMarker);

        private readonly IStatsProvider _statsProvider;

        public IEnumerable<OverviewPeriod> AvailablePeriods { get; } = (OverviewPeriod[])Enum.GetValues(typeof(OverviewPeriod));

        public ObservableCollection<CategoryStats> Stats { get; } = new();

        public OverviewPeriod SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                SetProperty(ref _selectedPeriod, value);
                UpdateStats();
            }
        }

        public Money Available
        {
            get => _available;
            set => SetProperty(ref _available, value);
        }

        public Money Incomes
        {
            get => _incomes;
            set => SetProperty(ref _incomes, value);
        }

        public Money Outcomes
        {
            get => _outcomes;
            set => SetProperty(ref _outcomes, value);
        }

        public Money TotalSpent
        {
            get => _totalSpent;
            set => SetProperty(ref _totalSpent, value);
        }

        private void UpdateStats()
        {
            Stats.Clear();
            var stats = _statsProvider.GetCategoryStatsFor(SelectedPeriod, this).ToList();
            stats.Sort((x, y) => y.Spent.CompareTo(x.Spent));

            foreach(var stat in stats)
            {
                Stats.Add(stat);
            }

            TotalSpent = new Money(Stats.Sum(x => x.Spent), Settings.Instance.CurrencyMarker);
        }
    }
}

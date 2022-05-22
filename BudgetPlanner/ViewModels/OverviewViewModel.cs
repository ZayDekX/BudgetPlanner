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

        private float _totalSpent;
        private float _incomes;
        private float _outcomes;
        private float _available;

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

        public float Available
        {
            get => _available;
            set => SetProperty(ref _available, value);
        }

        public float Incomes
        {
            get => _incomes;
            set => SetProperty(ref _incomes, value);
        }

        public float Outcomes
        {
            get => _outcomes;
            set => SetProperty(ref _outcomes, value);
        }

        public float TotalSpent
        {
            get => _totalSpent;
            set => SetProperty(ref _totalSpent, value);
        }

        private void UpdateStats()
        {
            Stats.Clear();
            var stats = _statsProvider.GetStats(SelectedPeriod).ToList();
            stats.Sort((x, y) => y.TotalSpent.CompareTo(x.TotalSpent));

            foreach(var stat in stats)
            {
                Stats.Add(stat);
            }

            TotalSpent = Stats.Sum(x => x.TotalSpent);
        }
    }
}

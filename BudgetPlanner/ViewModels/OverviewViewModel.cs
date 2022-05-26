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
        private string _selectedPeriodRange;
        private readonly IStatsProvider _statsProvider;

        public IEnumerable<OverviewPeriod> AvailablePeriods { get; } = (OverviewPeriod[])Enum.GetValues(typeof(OverviewPeriod));

        public ObservableCollection<CategoryStats> Stats { get; set; } = new();

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

        public string SelectedPeriodRange
        {
            get => _selectedPeriodRange;
            set => SetProperty(ref _selectedPeriodRange, value);
        }

        private void UpdateStats()
        {
            Stats.Clear();
            var stats = _statsProvider.GetCategoryStatsFor(SelectedPeriod, this).ToList();
            stats.Sort((x, y) => y.Spent.CompareTo(x.Spent));

            foreach (var stat in stats)
            {
                Stats.Add(stat);
            }

            SelectedPeriodRange = SelectedPeriod switch
            {
                OverviewPeriod.Day => "Today",
                OverviewPeriod.Week => $"{SelectedPeriodStart:dd.MM} - {SelectedPeriodStart.AddDays(7):dd.MM}",
                OverviewPeriod.Month => $"{SelectedPeriodStart:dd.MM} - {SelectedPeriodStart.AddMonths(1):dd.MM}",
                _ => $"{SelectedPeriodStart:dd.MM.yyyy} - {SelectedPeriodStart.AddYears(1):dd.MM.yyyy}",
            };

            TotalSpent = new Money(Stats.Sum(x => x.Spent), Settings.Instance.CurrencyMarker);
        }

        private DateTime SelectedPeriodStart => SelectedPeriod switch
        {
            OverviewPeriod.Week => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1),
            OverviewPeriod.Month => DateTime.Today.AddDays(-DateTime.Today.Day + 1),
            OverviewPeriod.Year => DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(-DateTime.Today.Month + 1),
            _ => DateTime.Today,
        };
    }
}

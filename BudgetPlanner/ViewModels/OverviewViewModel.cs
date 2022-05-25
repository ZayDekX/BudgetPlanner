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
        public OverviewViewModel(IDataProvider statsProvider)
        {
            _statsProvider = statsProvider;
            SelectedPeriod = OverviewPeriod.Day;
        }

        private OverviewPeriod _selectedPeriod;

        private Money _totalSpent = Money.Zero(Settings.CurrencyMarker);
        private Money _incomes = Money.Zero(Settings.CurrencyMarker);
        private Money _outcomes = Money.Zero(Settings.CurrencyMarker);
        private Money _available = Money.Zero(Settings.CurrencyMarker);
        private ObservableCollection<CategoryStats> _stats = new();
        private readonly IDataProvider _statsProvider;

        public IEnumerable<OverviewPeriod> AvailablePeriods { get; } = (OverviewPeriod[])Enum.GetValues(typeof(OverviewPeriod));

        public ObservableCollection<CategoryStats> Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value);
        }

        public OverviewPeriod SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                _ = SetProperty(ref _selectedPeriod, value);
                Update();
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

        public void Update()
        {
            var operations = _statsProvider.GetOperations().ToList();

            Available = new Money(operations.Select(o => o.Amount.Amount * (o.Category.OperationType is OperationType.Outcome ? -1 : 1)).Sum(), Settings.CurrencyMarker);

            Incomes = new(operations.Where(o => o.Category.OperationType is OperationType.Income && o.DateTime > SelectedPeriodStart).Select(o => o.Amount.Amount).Sum(), Settings.CurrencyMarker);
            Outcomes = new(operations.Where(o => o.Category.OperationType is OperationType.Outcome && o.DateTime > SelectedPeriodStart).Select(o => o.Amount.Amount).Sum(), Settings.CurrencyMarker);

            Stats = new(_statsProvider.GetCategoryStats(SelectedPeriodStart));

            TotalSpent = new Money(Stats.Sum(x => x.Spent), Settings.CurrencyMarker);
        }

        private DateTime SelectedPeriodStart => SelectedPeriod switch
        {
            OverviewPeriod.Day => DateTime.Today,
            OverviewPeriod.Week => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1),
            OverviewPeriod.Month => DateTime.Today.AddDays(-DateTime.Today.Day + 1),
            OverviewPeriod.Year => DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(-DateTime.Today.Month + 1),
            _ => DateTime.MinValue
        };
    }
}

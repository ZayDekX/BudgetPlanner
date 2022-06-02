using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BudgetPlanner.Data;
using BudgetPlanner.DataAccess.Providers;
using BudgetPlanner.Models;
using BudgetPlanner.Utils.Commands;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace BudgetPlanner.ViewModels.Implementation;

public class OverviewViewModel : ObservableObject, IOverviewViewModel
{
    public OverviewViewModel(IStatsProvider statsProvider)
    {
        UpdateCommand = new DispatcherCommand(Update);

        _statsProvider = statsProvider;

        SelectedPeriod = OverviewPeriod.Day;
    }

    private OverviewPeriod _selectedPeriod;

    private Money _totalSpent = Money.Zero;
    private Money _incomes = Money.Zero;
    private Money _outcomes = Money.Zero;
    private Money _available = Money.Zero;

    private string _selectedPeriodRange;
    private ObservableCollection<CategoryStats> _stats = new();

    public ICommand UpdateCommand { get; }

    private readonly IStatsProvider _statsProvider;

    public IEnumerable<OverviewPeriod> AvailablePeriods { get; } = (OverviewPeriod[])Enum.GetValues(typeof(OverviewPeriod));

    public ObservableCollection<CategoryStats> Stats
    {
        get => _stats;
        set => SetProperty(ref _stats, value);
    }

    public OverviewPeriod SelectedPeriod
    {
        get => _selectedPeriod;
        set => SetProperty(ref _selectedPeriod, value);
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

    private void Update()
    {
        Available = _statsProvider.GetAvailable();

        Incomes = _statsProvider.GetIncomes(SelectedPeriodStart);
        Outcomes = _statsProvider.GetOutcomes(SelectedPeriodStart);

        Stats = new(_statsProvider.GetCategoryStats(SelectedPeriodStart));

        SelectedPeriodRange = SelectedPeriod switch
        {
            OverviewPeriod.Day => "Today",
            OverviewPeriod.Week => $"{SelectedPeriodStart:dd.MM} - {SelectedPeriodStart.AddDays(7):dd.MM}",
            OverviewPeriod.Month => $"{SelectedPeriodStart:dd.MM} - {SelectedPeriodStart.AddMonths(1):dd.MM}",
            _ => $"{SelectedPeriodStart:dd.MM.yyyy} - {SelectedPeriodStart.AddYears(1):dd.MM.yyyy}",
        };

        TotalSpent = new Money(Stats.Sum(x => x.Spent));
    }

    private DateTime SelectedPeriodStart => SelectedPeriod switch
    {
        OverviewPeriod.Week => DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1),
        OverviewPeriod.Month => DateTime.Today.AddDays(-DateTime.Today.Day + 1),
        OverviewPeriod.Year => DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(-DateTime.Today.Month + 1),
        _ => DateTime.Today,
    };
}

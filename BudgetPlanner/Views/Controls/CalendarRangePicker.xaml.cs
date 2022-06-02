using System;

using System.Linq;

using Windows.UI.Xaml.Controls;

namespace BudgetPlanner.Views.Controls;
public sealed partial class CalendarRangePicker
{
    public CalendarRangePicker()
    {
        InitializeComponent();
    }

    private bool _updatingSelectedDates;
    private bool _updatedSelectedDates;

    public DateTimeOffset StartDate { get; private set; }

    public DateTimeOffset EndDate { get; private set; }

    public event Action<DateTimeOffset, DateTimeOffset> DateRangeChanged;

    public event Action DateRangeDeselected;

    private void UpdateSelectedPeriod(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        switch (sender.SelectedDates.Count)
        {
            case 0 when _updatingSelectedDates:

                sender.SelectedDates.Add(StartDate);
                return;

            case 1 when _updatingSelectedDates:

                _updatedSelectedDates = true;
                sender.SelectedDates.Add(EndDate);
                return;

            case 2 when _updatedSelectedDates:

                _updatingSelectedDates = false;

                StartDate = sender.SelectedDates.Min();
                EndDate = sender.SelectedDates.Max();

                DateRangeChanged?.Invoke(StartDate, EndDate);
                return;

            case 0:

                DateRangeDeselected?.Invoke();
                return;

            case 1:

                if (sender.SelectedDates[0].Date == DateTime.Today.Date)
                {
                    sender.SelectedDates.RemoveAt(0);
                    return;
                }

                var dates = new[] { sender.SelectedDates[0], DateTime.Today };

                StartDate = dates.Min();
                EndDate = dates.Max();

                DateRangeChanged?.Invoke(StartDate, EndDate);
                return;

            default:

                StartDate = sender.SelectedDates.Min();
                EndDate = sender.SelectedDates.Max();

                _updatingSelectedDates = true;
                _updatedSelectedDates = false;

                sender.SelectedDates.Clear();
                return;
        }
    }
}

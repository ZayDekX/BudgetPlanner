#nullable enable

using System;
using System.Windows.Input;

using Windows.UI.Core;
using Windows.UI.Xaml;

namespace BudgetPlanner.Utils.Commands;

public class DispatcherCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public DispatcherCommand(Action execute)
    {
        _execute = execute;
    }

    public DispatcherCommand(Action execute, Func<bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() is not false;
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => _execute.Invoke());
    }
}

public class DispatcherCommand<TArgs> : ICommand
{
    private readonly Action<TArgs?> _execute;
    private readonly Func<TArgs?, bool>? _canExecute;

    public DispatcherCommand(Action<TArgs?> execute)
    {
        _execute = execute;
    }

    public DispatcherCommand(Action<TArgs?> execute, Func<TArgs?, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke((TArgs?)parameter) is not false;
    }

    public async void Execute(object parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => _execute.Invoke((TArgs?)parameter));
    }
}
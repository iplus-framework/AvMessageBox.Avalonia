using System.Windows.Input;

namespace MsBox.Avalonia.ViewModels.Commands;

public class RelayCommand : ICommand
{
    private readonly Func<object?, bool>? canExecute;
    private readonly Action<object?> execute;

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { }
        remove { }
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute == null || canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        execute(parameter);
    }
}
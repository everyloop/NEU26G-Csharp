
using System.Windows;
using System.Windows.Input;

namespace L049_Windows_in_MVVM;

public class AsyncRelayCommand : ICommand
{
    private readonly Func<object?, Task> _execute;
    private bool _isExecuting;

    public AsyncRelayCommand(Func<object?, Task> execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return !_isExecuting;
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
            return;

        _isExecuting = true;
        RaiseCanExecuteChanged();

        try
        {
            await _execute(parameter);
        }
        catch (Exception ex)
        {
            // Förenklad felhantering för demonstrationssyfte.
            MessageBox.Show(ex.Message, "Error");
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
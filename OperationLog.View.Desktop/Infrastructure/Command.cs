using System;
using System.Windows.Input;

namespace OperationLog.Presentation.Desktop.Infrastructure
{
    public class Command : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public Command(Action<object> execute)
            : this(execute, null)
        {
        }

        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) => _execute(parameter);
    }
}

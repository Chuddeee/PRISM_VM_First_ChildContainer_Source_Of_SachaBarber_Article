using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PrismViewModelFirst
{
    public class SimpleCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public SimpleCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public SimpleCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        protected virtual void raiseCanExecuteChanged(EventArgs e)
        {
            var handler = this.CanExecuteChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

    }

}

using System;
using System.Windows.Input;

namespace OpenTourClient.ViewModels
{
    public class CommandHandler : ICommand
    {
        private Func<object, object> function;
        private bool canExecute;

        public CommandHandler(Func<object, object> function, bool canExecute)
        {
            this.function = function;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        public void Execute(object parameter)
        {
            this.function(parameter);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action _execute;
        private bool _canExecute;

        public Command(Action execute)
        {
            _execute = execute;
            _canExecute = true;
        }

        public Command(Action execute, bool canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}

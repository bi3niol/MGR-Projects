using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectLAB.UserControls.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecuteAction = null)
        {
            _execute = executeAction ?? DefaultExecute;
            _canExecute = canExecuteAction ?? DefaultPredicate;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        private static void DefaultExecute(object o) { }
        private static bool DefaultPredicate(object o) => true;
    }
}

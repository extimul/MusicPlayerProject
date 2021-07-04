using MusicPlayerProject.Core.Managers.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicPlayerProject.Core.Commands
{
    public class RenavigateCommand : ICommand
    {
        private readonly INavigator _navigator;

        public event EventHandler CanExecuteChanged;

        public RenavigateCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return _navigator.CanGoBack;
        }

        public void Execute(object parameter)
        {
            _navigator.CurrentViewModel = _navigator.PreviousViewModel;
        }
    }
}

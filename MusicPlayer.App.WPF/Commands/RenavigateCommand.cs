using MusicPlayer.Core.Services.Navigators;
using System;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.Commands
{
    public class RenavigateCommand : ICommand
    {
        private readonly INavigatorService _navigator;

        public event EventHandler CanExecuteChanged;

        public RenavigateCommand(INavigatorService navigator)
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

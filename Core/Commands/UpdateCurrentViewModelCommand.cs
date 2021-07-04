using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace MusicPlayerProject.Core.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        private IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return _navigator.CanGoForward;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewTypes viewType)
            {
                _navigator.PreviousViewModel = _navigator.CurrentViewModel;
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}

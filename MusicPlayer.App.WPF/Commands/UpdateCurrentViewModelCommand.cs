using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.Types;
using System;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigatorService _navigator;

        private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigatorService navigator, IViewModelFactory viewModelFactory)
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

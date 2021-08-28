using MusicPlayer.App.WPF.ViewModels.Base;
using System;

namespace MusicPlayer.App.WPF.Services.Navigators
{
    public sealed class NavigatorService : INavigatorService
    {
        private ViewModelBase _previousViewModel;
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();

                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public ViewModelBase PreviousViewModel
        {
            get => _previousViewModel;
            set
            {
                _previousViewModel?.Dispose();
                _previousViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public bool CanGoForward => CurrentViewModel != null;
        public bool CanGoBack => PreviousViewModel != null;

        public event Action StateChanged;
    }
}

using MusicPlayer.App.WPF.ViewModels.Base;
using System;

namespace MusicPlayer.App.WPF.Services.Navigators
{
    public interface INavigatorService
    {
        event Action StateChanged;
        ViewModelBase PreviousViewModel { get; set; }
        ViewModelBase CurrentViewModel { get; set; }
        bool CanGoForward { get; }
        bool CanGoBack { get; }
    }
}

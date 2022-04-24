using MusicPlayer.Core.MVVMBase;
using System;

namespace MusicPlayer.Core.Services.Navigators
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

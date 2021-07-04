using MusicPlayerProject.ViewModels.Base;
using System;

namespace MusicPlayerProject.Core.Managers.Navigators
{
    public interface INavigator
    {
        event Action StateChanged;
        ViewModelBase PreviousViewModel { get; set; }
        ViewModelBase CurrentViewModel { get; set; }
        bool CanGoForward { get; }
        bool CanGoBack { get; }
    }
}

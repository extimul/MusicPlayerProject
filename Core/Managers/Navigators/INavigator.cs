using MusicPlayerProject.ViewModels.Base;
using System;

namespace MusicPlayerProject.Core.Managers.Navigators
{
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}

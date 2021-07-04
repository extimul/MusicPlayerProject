using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;

namespace MusicPlayerProject.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewTypes viewType);
        ViewModelBase CreatePlaylistViewModel(Playlist playlist, INavigator navigator);
    }
}

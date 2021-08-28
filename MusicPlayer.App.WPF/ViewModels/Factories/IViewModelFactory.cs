using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;

namespace MusicPlayer.App.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewTypes viewType);
        ViewModelBase CreatePlaylistViewModel(Playlist playlist, IAudioService audioService, IIconManager iconManager, INavigatorService navigator, IContentManager<Playlist> contentManager);
    }
}

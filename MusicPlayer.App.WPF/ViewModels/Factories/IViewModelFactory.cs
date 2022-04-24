using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.Services.Audio;
using MusicPlayer.Core.Services.Content;
using MusicPlayer.Core.Services.Icon;
using MusicPlayer.Core.Services.Navigators;
using MusicPlayer.Core.Types;

namespace MusicPlayer.App.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewTypes viewType);
        ViewModelBase CreatePlaylistViewModel(Playlist playlist,
                                              IAudioService audioService,
                                              IIconManager iconManager,
                                              INavigatorService navigator,
                                              IContentManager<Playlist, Library> contentManager,
                                              IContentManager<Track, Playlist> trackManager,
                                              IDataPathService pathService);
    }
}

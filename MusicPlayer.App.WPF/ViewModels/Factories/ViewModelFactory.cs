using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using System;

namespace MusicPlayer.App.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<LibraryViewModel> _createMusicLibraryViewModel;
        private readonly CreateViewModel<QueueViewModel> _createQueueViewModel;

        public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<LibraryViewModel> createMusicLibraryViewModel,
            CreateViewModel<QueueViewModel> createQueueViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createMusicLibraryViewModel = createMusicLibraryViewModel;
            _createQueueViewModel = createQueueViewModel;
        }

        public ViewModelBase CreateViewModel(ViewTypes viewType)
        {
            return viewType switch
            {
                ViewTypes.Home => _createHomeViewModel(),
                ViewTypes.Library => _createMusicLibraryViewModel(),
                ViewTypes.Queue => _createQueueViewModel(),
                _ => throw new ArgumentException("ViewType does not have a ViewModel", nameof(viewType)),
            };
        }

        public ViewModelBase CreatePlaylistViewModel(Playlist playlist,
                                                     IAudioService audioService,
                                                     IIconManager iconManager,
                                                     INavigatorService navigator,
                                                     IContentManager<Playlist> contentManager)
        {
            return new PlaylistViewModel(playlist, audioService, iconManager, navigator, contentManager);
        }
    }
}

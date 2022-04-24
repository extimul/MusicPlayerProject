using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase.Commands;
using MusicPlayer.Core.Services.Audio;
using MusicPlayer.Core.Services.Content;
using MusicPlayer.Core.Services.Icon;
using MusicPlayer.Core.Services.Navigators;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class OpenPlaylistCommand : AsyncCommandBase
    {
        private readonly LibraryViewModel viewModel;
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        private readonly INavigatorService navigator;
        private readonly IViewModelFactory viewModelFactory;
        private readonly IContentManager<Playlist, Library> contentManager;
        private readonly IContentManager<Track, Playlist> tracksManager;
        private readonly IDataPathService pathService;

        public OpenPlaylistCommand(LibraryViewModel viewModel,
                                    IAudioService audioService,
                                    IIconManager iconManager,
                                    INavigatorService navigator,
                                    IViewModelFactory viewModelFactory,
                                    IContentManager<Playlist, Library> contentManager,
                                    IContentManager<Track, Playlist> tracksManager,
                                    IDataPathService pathService)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
            this.contentManager = contentManager;
            this.tracksManager = tracksManager;
            this.pathService = pathService;
        }

        public override Task ExecuteAsync(object parameter)
        {
            navigator.PreviousViewModel = navigator.CurrentViewModel;
            navigator.CurrentViewModel = viewModelFactory.CreatePlaylistViewModel(viewModel.SelectedPlaylist, audioService, iconManager, navigator, contentManager, tracksManager, pathService);
            return Task.CompletedTask;
        }
    }
}

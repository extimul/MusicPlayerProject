using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.Models;
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
        private readonly IContentManager<Playlist> tracksCollectionService;

        public OpenPlaylistCommand(LibraryViewModel viewModel,
                                    IAudioService audioService,
                                    IIconManager iconManager,
                                    INavigatorService navigator,
                                    IViewModelFactory viewModelFactory,
                                    IContentManager<Playlist> tracksCollectionService)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
            this.tracksCollectionService = tracksCollectionService;
        }

        public override Task ExecuteAsync(object parameter)
        {
            navigator.PreviousViewModel = navigator.CurrentViewModel;
            navigator.CurrentViewModel = viewModelFactory.CreatePlaylistViewModel(viewModel.SelectedPlaylist, audioService, iconManager, navigator, tracksCollectionService);
            return Task.CompletedTask;
        }
    }
}

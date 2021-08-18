using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.ViewModels.Factories;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class OpenPlaylistCommand : AsyncCommandBase
    {
        private readonly LibraryViewModel viewModel;
        private readonly IAudioService audioService;
        private readonly INavigatorService navigator;
        private readonly IViewModelFactory viewModelFactory;
        private readonly IDataPathService pathService;

        public OpenPlaylistCommand(LibraryViewModel viewModel, IAudioService audioService, INavigatorService navigator, IViewModelFactory viewModelFactory, IDataPathService pathService)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
            this.pathService = pathService;
        }

        public override Task ExecuteAsync(object parameter)
        {
            navigator.PreviousViewModel = navigator.CurrentViewModel;
            navigator.CurrentViewModel = viewModelFactory.CreatePlaylistViewModel(viewModel.SelectedPlaylist, audioService, navigator, pathService);
            return Task.CompletedTask;
        }
    }
}

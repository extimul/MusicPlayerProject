using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
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
        private readonly IIconManager iconManager;
        private readonly INavigatorService navigator;
        private readonly IViewModelFactory viewModelFactory;

        public OpenPlaylistCommand(LibraryViewModel viewModel, IAudioService audioService, IIconManager iconManager, INavigatorService navigator, IViewModelFactory viewModelFactory)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public override Task ExecuteAsync(object parameter)
        {
            navigator.PreviousViewModel = navigator.CurrentViewModel;
            navigator.CurrentViewModel = viewModelFactory.CreatePlaylistViewModel(viewModel.SelectedPlaylist, audioService, iconManager, navigator);
            return Task.CompletedTask;
        }
    }
}

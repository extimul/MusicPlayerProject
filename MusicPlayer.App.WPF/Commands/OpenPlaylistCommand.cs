using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.ViewModels.Factories;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class OpenPlaylistCommand : AsyncCommandBase
    {
        private readonly INavigatorService _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IDataPathService pathService;
        private readonly LibraryViewModel _libraryViewModel;

        public OpenPlaylistCommand(LibraryViewModel libraryViewModel, INavigatorService navigator, IViewModelFactory viewModelFactory, IDataPathService pathService)
        {
            _libraryViewModel = libraryViewModel;
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            this.pathService = pathService;
        }

        public override Task ExecuteAsync(object parameter)
        {
            if (_libraryViewModel.SelectedPlaylist != null)
            {
                _navigator.PreviousViewModel = _navigator.CurrentViewModel;
                _navigator.CurrentViewModel = _viewModelFactory.CreatePlaylistViewModel(_libraryViewModel.SelectedPlaylist, _navigator, pathService);
            }
            return Task.CompletedTask;
        }
    }
}

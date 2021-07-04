using MusicPlayerProject.Core.Commands.Base;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.ViewModels;
using MusicPlayerProject.ViewModels.Factories;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Commands
{
    public class OpenPlaylistCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly LibraryViewModel _libraryViewModel;

        public OpenPlaylistCommand(LibraryViewModel libraryViewModel, INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _libraryViewModel = libraryViewModel;
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_libraryViewModel.SelectedPlaylist != null)
            {
                _navigator.PreviousViewModel = _navigator.CurrentViewModel;
                _navigator.CurrentViewModel = _viewModelFactory.CreatePlaylistViewModel(_libraryViewModel.SelectedPlaylist, _navigator);
            }
        }
    }
}

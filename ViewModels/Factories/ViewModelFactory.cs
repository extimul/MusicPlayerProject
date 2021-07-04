using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System;

namespace MusicPlayerProject.ViewModels.Factories
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
            switch (viewType)
            {
                case ViewTypes.Home:
                    return _createHomeViewModel();
                case ViewTypes.Library:
                    return _createMusicLibraryViewModel();
                case ViewTypes.Queue:
                    return _createQueueViewModel();
                default:
                    throw new ArgumentException("ViewType does not have a ViewModel", "viewType");
            }
        }

        public ViewModelBase CreatePlaylistViewModel(Playlist playlist, INavigator navigator)
        {
            return new PlaylistViewModel(playlist, navigator);
        }
    }
}

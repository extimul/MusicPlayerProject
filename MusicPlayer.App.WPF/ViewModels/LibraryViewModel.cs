using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        #region Properties
        public IPlaylistService PlaylistManager { get; }
        public ObservableCollection<Playlist> Playlist => PlaylistManager.PlaylistsCollection;
        private Playlist _selectedPlaylist;
        private readonly INavigatorService navigator;
        private readonly IViewModelFactory viewModelFactory;
        private readonly IDataPathService pathService;

        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                if (value == null)
                {
                    _selectedPlaylist = null;
                    return;
                }
                if (value.Equals(_selectedPlaylist)) return;
                _selectedPlaylist = value;
                OnPropertyChanged(nameof(SelectedPlaylist));
            }
        }

        #endregion

        #region Commands
        public ICommand SortCommand { get; }
        public ICommand CreatePlaylistCommand { get; }
        public ICommand OpenPlaylistCommand { get; }
        #endregion

        public LibraryViewModel(IPlaylistService playlistManager, 
                                INavigatorService navigator,
                                IViewModelFactory viewModelFactory,
                                IDataPathService pathService)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
            this.pathService = pathService;
            PlaylistManager = playlistManager;
            PlaylistManager.StateChanged += OnStateChanged;

            SortCommand = new SortPlaylistsCommand();
            CreatePlaylistCommand = new CreatePlaylistCommand(this, pathService);
            OpenPlaylistCommand = new OpenPlaylistCommand(this, navigator, viewModelFactory, pathService);
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(Playlist));
        }

        public override void Dispose()
        {
            PlaylistManager.StateChanged -= OnStateChanged;
            base.Dispose();
        }
    }
}

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
        #region Fields
        private Playlist _selectedPlaylist;
        private readonly IPlaylistService playlistManager;
        #endregion

        #region Properties
        public ObservableCollection<Playlist> PlaylistCollection => playlistManager.PlaylistsCollection;

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
                                IDataPathService pathService,
                                IAudioService audioService)
        {
            this.playlistManager = playlistManager;

            this.playlistManager.PlaylistCollectionChanged += OnPlaylistCollectionChanged;

            SortCommand = new SortPlaylistsCommand();
            CreatePlaylistCommand = new CreatePlaylistCommand(this, pathService, playlistManager);
            OpenPlaylistCommand = new OpenPlaylistCommand(this, audioService, navigator, viewModelFactory, pathService);
        }

        private void OnPlaylistCollectionChanged()
        {
            OnPropertyChanged(nameof(PlaylistCollection));
        }

        public override void Dispose()
        {
            playlistManager.PlaylistCollectionChanged -= OnPlaylistCollectionChanged;
            base.Dispose();
        }
    }
}

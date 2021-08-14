using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
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
        public IPlaylistManager PlaylistManager { get; }
        public ObservableCollection<Playlist> Playlist => PlaylistManager.PlaylistsCollection;
        private Playlist _selectedPlaylist;

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

        public LibraryViewModel(IPlaylistManager playlistManager, INavigatorService navigator, IViewModelFactory viewModelFactory)
        {
            PlaylistManager = playlistManager;
            PlaylistManager.StateChanged += OnStateChanged;

            SortCommand = new SortPlaylistsCommand();
            CreatePlaylistCommand = new CreatePlaylistCommand(this);
            OpenPlaylistCommand = new OpenPlaylistCommand(this, navigator, viewModelFactory);
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

using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using MusicPlayerProject.ViewModels.Factories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
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

        public LibraryViewModel(IPlaylistManager playlistManager, INavigator navigator, IViewModelFactory viewModelFactory)
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

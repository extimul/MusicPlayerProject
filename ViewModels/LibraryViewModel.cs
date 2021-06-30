using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        public IPlaylistManager PlaylistManager { get; }

        public ObservableCollection<Playlist> Playlist => PlaylistManager.PlaylistsCollection;

        public ICommand SortCommand { get; }
        public ICommand CreatePlaylistCommand { get; }

        public LibraryViewModel(IPlaylistManager playlistManager)
        {
            PlaylistManager = playlistManager;
            PlaylistManager.StateChanged += OnStateChanged;

            SortCommand = new SortPlaylistsCommand();
            CreatePlaylistCommand = new CreatePlaylistCommand(this);

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

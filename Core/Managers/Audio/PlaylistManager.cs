using MusicPlayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public class PlaylistManager : IPlaylistManager
    {
        #region Events
        public event Action StateChanged;
        #endregion

        #region Fields
        private ObservableCollection<Playlist> _playlistCollection;
        private ObservableCollection<Track> _queueplaylist;
        #endregion

        #region Properties
        public ObservableCollection<Playlist> PlaylistsCollection
        {
            get => _playlistCollection;
            set
            {
                if (value.Equals(_playlistCollection)) return;
                _playlistCollection = value;
                StateChanged?.Invoke();
            }
        }

        public ObservableCollection<Track> QueuePlaylist
        {
            get => _queueplaylist;
            set
            {
                if (value.Equals(_queueplaylist)) return;
                _queueplaylist = value;
                StateChanged?.Invoke();
            }
        }
        #endregion

        public PlaylistManager()
        {
            QueuePlaylist = new ObservableCollection<Track>();
            Task.Run(async () => await LoadAllPlaylists());
        }

        public Task LoadQueue()
        {
            throw new NotImplementedException();
        }

        public Task LoadAllPlaylists()
        {
            PlaylistsCollection = new ObservableCollection<Playlist>();
            PlaylistsCollection.Add(new Playlist()
            {
                Id = 1,
                PlaylistName = "Liked songs",
                Description = "Something text for this playlist and text text text text text",
                RecentlyPlay = DateTime.Now,
                AddedDate = DateTime.Now,
                Author = "You",
                ImageSource = @"E:\Projects\VisualStudioProjects\MusicPlayerProject\ApplicationResources\DefaultSongImg.png"
            });
            return Task.CompletedTask;
        }

        public Task AddPlaylist(Playlist playlist)
        {
            if (PlaylistsCollection != null)
            {
                playlist.Id = PlaylistsCollection[^1].Id + 1;
                PlaylistsCollection.Add(playlist);
            }
            StateChanged?.Invoke();
            return Task.CompletedTask;
        }

        public Task DeletePlaylist(int id)
        {
            throw new NotImplementedException();
        }

        public Task ChangePlaylist(Playlist playlist)
        {
            throw new NotImplementedException();
        }
    }
}

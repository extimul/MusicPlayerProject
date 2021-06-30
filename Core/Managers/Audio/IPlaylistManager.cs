using MusicPlayerProject.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public interface IPlaylistManager
    {
        public event Action StateChanged;
        public ObservableCollection<Track> QueuePlaylist { get; set; }
        public ObservableCollection<Playlist> PlaylistsCollection { get; set; }
        Task LoadQueue();
        Task LoadAllPlaylists();
        Task AddPlaylist(Playlist playlist);
        Task DeletePlaylist(int id);
        Task ChangePlaylist(Playlist playlist);
    }
}

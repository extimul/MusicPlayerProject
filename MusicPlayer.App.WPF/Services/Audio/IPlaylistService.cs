using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Audio
{
    public interface IPlaylistService
    {
        public event Action QueuePlaylistChanged;
        public event Action PlaylistCollectionChanged;
        public ObservableCollection<Track> QueuePlaylist { get; set; }
        public ObservableCollection<Playlist> PlaylistsCollection { get; set; }
        Task LoadQueue();
        Task LoadAllPlaylists();
        Task AddPlaylist(Playlist playlist);
        Task DeletePlaylist(int id);
        Task ChangePlaylist(Playlist playlist);
    }
}

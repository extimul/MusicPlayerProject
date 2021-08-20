using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Audio
{
    public class PlaylistService : IPlaylistService
    {
        #region Events
        public event Action QueuePlaylistChanged;
        public event Action PlaylistCollectionChanged;
        #endregion

        #region Fields
        private readonly IDataPathService dataPathService;

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
                PlaylistCollectionChanged?.Invoke();
            }
        }

        public ObservableCollection<Track> QueuePlaylist
        {
            get => _queueplaylist;
            set
            {
                if (value.Equals(_queueplaylist)) return;
                _queueplaylist = value;
                QueuePlaylistChanged?.Invoke();
            }
        }
        #endregion

        public PlaylistService(IDataPathService dataPathService)
        {
            this.dataPathService = dataPathService;

            LoadQueue();
            LoadAllPlaylists();
        }

        public Task LoadQueue()
        {
            QueuePlaylist = new ObservableCollection<Track>()
            {
                new Track()
                {
                    Id = 1,
                    Title = "Rain",
                    TrackAlbum = "Rain",
                    Author = "grandson",
                    IsLiked = true,
                    TrackImage = dataPathService.DefaultTrackImage,
                    TrackSource = dataPathService.MusicContainerPath + "\\track1.mp3",
                    Duration = TimeSpan.FromSeconds(1000)
                },
                new Track()
                {
                    Id = 2,
                    Title = "Жизнь поёт",
                    TrackAlbum = "Album",
                    Author = "Monatik",
                    IsLiked = true,
                    TrackImage = dataPathService.DefaultTrackImage,
                    TrackSource = dataPathService.MusicContainerPath + "\\track2.mp3",
                    Duration = TimeSpan.FromSeconds(300)
                },
                new Track()
                {
                    Id = 3,
                    Title = "Я в моменте",
                    TrackAlbum = "Я в моменте",
                    Author = "Джарахов & Markul",
                    IsLiked = true,
                    TrackImage = dataPathService.DefaultTrackImage,
                    TrackSource = dataPathService.MusicContainerPath + "\\track3.mp3",
                    Duration = TimeSpan.FromSeconds(300)
                },
                new Track()
                {
                    Id = 4,
                    Title = "Arcade",
                    TrackAlbum = "Arcade",
                    Author = "Duncan Laurence",
                    IsLiked = true,
                    TrackImage = dataPathService.DefaultTrackImage,
                    TrackSource = dataPathService.MusicContainerPath + "\\track4.mp3",
                    Duration = TimeSpan.FromSeconds(303)
                },
                new Track()
                {
                    Id = 5,
                    Title = "Люби меня люби",
                    TrackAlbum = "Звезды только ночью",
                    Author = "Гречка",
                    IsLiked = true,
                    TrackImage = dataPathService.DefaultTrackImage,
                    TrackSource = dataPathService.MusicContainerPath + "\\track5.mp3",
                    Duration = TimeSpan.FromSeconds(303)
                },
                new Track()
                {
                    Id = 6,
                    Title = "Spirits",
                    TrackAlbum = "Album",
                    Author = "Kabes",
                    IsLiked = true,
                    TrackImage = dataPathService.DefaultTrackImage,
                    TrackSource = dataPathService.MusicContainerPath + "\\track6.mp3",
                    Duration = TimeSpan.FromSeconds(300)
                }
            };
            QueuePlaylistChanged?.Invoke();
            return Task.CompletedTask;
        }

        public Task LoadAllPlaylists()
        {
            PlaylistsCollection = new ObservableCollection<Playlist>
            {
                new Playlist()
                {
                    Id = 1,
                    Title = "Liked songs",
                    Description = "Something text for this playlist and text text text text text",
                    RecentlyPlay = DateTime.Now,
                    AddedDate = DateTime.Now,
                    Author = "You",
                    ImageSource = dataPathService.DefaultTrackImage,
                    Tracks = new ObservableCollection<Track>()
                    {
                        new Track()
                        {
                            Id = 1,
                            Title = "A",
                            Author = "Author",
                            IsLiked = true,
                            TrackAlbum = "Album",
                            Duration = TimeSpan.FromSeconds(300),
                            TrackImage = dataPathService.DefaultTrackImage,
                            TrackSource = Track.GetFullTrackPath(dataPathService.MusicContainerPath, "track.mp3")
                        },
                        new Track()
                        {
                            Id = 2,
                            Title = "B",
                            Author = "Author",
                            IsLiked = true,
                            TrackAlbum = "Album",
                            Duration = TimeSpan.FromSeconds(300),
                            TrackImage = dataPathService.DefaultTrackImage,
                            TrackSource = Track.GetFullTrackPath(dataPathService.MusicContainerPath, "track2.mp3")
                        },
                        new Track()
                        {
                            Id = 3,
                            Title = "C",
                            Author = "Author",
                            IsLiked = true,
                            TrackAlbum = "Album",
                            Duration = TimeSpan.FromSeconds(300),
                            TrackImage = dataPathService.DefaultTrackImage,
                            TrackSource = Track.GetFullTrackPath(dataPathService.MusicContainerPath, "track3.mp3")
                        }
                    }
                }
            };
            PlaylistCollectionChanged?.Invoke();
            return Task.CompletedTask;
        }

        public Task AddPlaylist(Playlist playlist)
        {
            if (PlaylistsCollection != null)
            {
                playlist.Id = PlaylistsCollection[^1].Id + 1;
                PlaylistsCollection.Add(playlist);
            }
            PlaylistCollectionChanged?.Invoke();
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

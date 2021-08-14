using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        #region Fields
        private Playlist playlist;
        private readonly INavigatorService navigator;
        private readonly IDataPathService pathService;

        #endregion

        #region Properties
        public Playlist CurrentPlaylist
        {
            get => playlist;
            set
            {
                if (value.Equals(playlist)) return;
                playlist = value;
                OnPropertyChanged(nameof(CurrentPlaylist));
            }
        }

        #endregion

        #region Commands
        public ICommand GoBackCommand { get; }

        #endregion

        public PlaylistViewModel(Playlist playlist, INavigatorService navigator, IDataPathService pathService)
        {
            CurrentPlaylist = playlist;
            this.navigator = navigator;
            this.pathService = pathService;
            GoBackCommand = new RenavigateCommand(navigator);
            CurrentPlaylist = new Playlist()
            {
                Id = 1,
                PlaylistName = "Liked songs",
                Description = "Something text for this playlist and text text text text text text text text text text text text text text text text text text text text",
                RecentlyPlay = DateTime.Now,
                AddedDate = DateTime.Now,
                Author = "Large author name",
                ImageSource = @"E:\Projects\VisualStudioProjects\MusicPlayer.App.WPF\ApplicationResources\DefaultSongImg.png" ?? pathService.DefaultTrackImage,
                Tracks = new ObservableCollection<Track>()
                {
                    new Track()
                    {
                        Id = 1,
                        TrackTitle = "TrackTitle",
                        Author = "Author",
                        IsLiked = true,
                        TrackAlbum = "Album",
                        Duration = TimeSpan.FromSeconds(300),
                        TrackSource = @"E:\Projects\VisualStudioProjects\MusicPlayer.App.WPF\ApplicationResources\track.mp3"
                    }
                }
            };
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
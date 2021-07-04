using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Commands.Base;
using MusicPlayerProject.Core.Helpers;
using MusicPlayerProject.Core.Managers.Navigators;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        #region Fields
        private Playlist playlist;

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

        public PlaylistViewModel(Playlist playlist, INavigator navigator)
        {
            CurrentPlaylist = playlist;
            GoBackCommand = new RenavigateCommand(navigator);
        }

        public PlaylistViewModel()
        {
            CurrentPlaylist = new Playlist()
            {
                Id = 1,
                PlaylistName = "Liked songs",
                Description = "Something text for this playlist and text text text text text",
                RecentlyPlay = DateTime.Now,
                AddedDate = DateTime.Now,
                Author = "You",
                ImageSource = @"E:\Projects\VisualStudioProjects\MusicPlayerProject\ApplicationResources\DefaultSongImg.png" ?? PathHelper.GetDefaultImagePath()
            };
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
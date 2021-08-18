using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
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
        private readonly IAudioService audioService;
        private readonly INavigatorService navigator;
        private readonly IDataPathService pathService;
        #endregion

        #region Properties
        public Playlist CurrentPlaylist
        {
            get => playlist;
            set => SetField(ref playlist, value);
        }

        public Track SelectedTrack
        {
            get => audioService.SelectedTrack;
            set
            {
                audioService.SelectedTrack = value;
                OnPropertyChanged(nameof(SelectedTrack));
            }
        }

        #endregion

        #region Commands
        public ICommand GoBackCommand { get; }

        #endregion

        public PlaylistViewModel(Playlist playlist, IAudioService audioService, INavigatorService navigator, IDataPathService pathService)
        {
            CurrentPlaylist = playlist;
            this.audioService = audioService;
            this.navigator = navigator;
            this.pathService = pathService;

            this.audioService.SelectedTrack = (CurrentPlaylist.Tracks as ObservableCollection<Track>)[0];
            this.audioService.ActivePlaylist = (ObservableCollection<Track>)CurrentPlaylist.Tracks;
            this.audioService.TrackChanged += OnTrackChanged;

            GoBackCommand = new RenavigateCommand(navigator);
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
        }

        public override void Dispose()
        {
            this.audioService.TrackChanged -= OnTrackChanged;
            GC.SuppressFinalize(this);
        }
    }
}
using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Interfaces;
using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class PlaylistViewModel : ViewModelBase, ITracksListView
    {
        #region Fields
        private Playlist playlist;
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        #endregion

        #region Properties
        public Playlist CurrentPlaylist
        {
            get => playlist;
            set => SetField(ref playlist, value);
        }

        public ObservableCollection<Track> TracksCollection => ControlBarViewModel.FilteredCollection;

        public Track SelectedTrack
        {
            get => audioService.SelectedTrack;
            set
            {
                audioService.SelectedTrack = value;
                OnPropertyChanged(nameof(SelectedTrack));
            }
        }

        public PlaylistControlBarViewModel ControlBarViewModel { get; set; }

        public DrawingBrush PlayPauseIcon => iconManager.PlayPauseIcon;

        #endregion

        #region Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand PlayPauseCommand { get; set; }

        #endregion

        public PlaylistViewModel(Playlist playlist, IAudioService audioService, IIconManager iconManager, INavigatorService navigator)
        {
            CurrentPlaylist = playlist;
            ControlBarViewModel = new PlaylistControlBarViewModel(this);
            ControlBarViewModel.PropertyChanged += ControlBarViewModel_PropertyChanged;

            this.audioService = audioService;
            this.iconManager = iconManager;
            
            this.audioService.ActivePlaylist = TracksCollection;
            this.audioService.SelectedTrack = (TracksCollection.Count > 0) ? TracksCollection[0] : null;
            this.audioService.TrackChanged += OnTrackChanged;
            this.audioService.IconChanged += OnIconChanged;

            GoBackCommand = new RenavigateCommand(navigator);
            PlayPauseCommand = new PlayerControlsCommand(audioService, this);
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            OnPropertyChanged(nameof(PlayPauseIcon));
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
        }

        private void ControlBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TracksCollection));
            OnPropertyChanged(nameof(SelectedTrack));
        }

        public override void Dispose()
        {
            audioService.IconChanged -= OnIconChanged;
            audioService.TrackChanged -= OnTrackChanged;
            ControlBarViewModel.PropertyChanged -= ControlBarViewModel_PropertyChanged;
            ControlBarViewModel.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
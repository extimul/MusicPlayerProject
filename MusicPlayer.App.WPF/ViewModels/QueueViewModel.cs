using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Interfaces;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class QueueViewModel : ViewModelBase, ITracksListView
    {
        #region Fields
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        private readonly ITracksCollectionService<Track> tracksCollectionService;
        #endregion

        #region Properties
        public Track SelectedTrack
        {
            get => audioService.SelectedTrack;
            set
            {
                audioService.SelectedTrack = value;
                OnPropertyChanged(nameof(SelectedTrack));
            }
        }

        public ObservableCollection<Track> TracksCollection => tracksCollectionService.TracksCollection;
        public DrawingBrush PlayPauseIcon => iconManager.PlayPauseIcon;

        #endregion

        #region Commands
        public ICommand PlayPauseCommand { get; set; }
        #endregion

        public QueueViewModel(IAudioService audioService,
                              IIconManager iconManager, 
                              ITracksCollectionService<Track> playlistService)
        {
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.tracksCollectionService = playlistService;

            this.audioService.IconChanged += OnIconChanged;
            this.audioService.TrackChanged += OnTrackChanged;

            this.audioService.ActivePlaylist = TracksCollection;
            this.tracksCollectionService.CollectionChanged += OnQueueCollectionChanged;
            this.audioService.SelectedTrack = (TracksCollection?.Count > 0) ? TracksCollection[0] : null;

            PlayPauseCommand = new PlayerControlsCommand(audioService, this);
        }

        private void OnQueueCollectionChanged()
        {
            OnPropertyChanged(nameof(TracksCollection));
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            if (e?.SourceState is SourceTypes.TogglePlaybackSource)
            {
                iconManager.PlayPauseIcon = iconManager.SetPlayPauseIcon((PlaybackState)e.Value);
                OnPropertyChanged(nameof(PlayPauseIcon));
            }
        }

        public override void Dispose()
        {
            audioService.IconChanged -= OnIconChanged;
            audioService.TrackChanged -= OnTrackChanged;
            tracksCollectionService.CollectionChanged -= OnQueueCollectionChanged;
            base.Dispose();
        }
    }
}

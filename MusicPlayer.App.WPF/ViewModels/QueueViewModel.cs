using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;
using System.Collections.ObjectModel;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class QueueViewModel : ViewModelBase
    {
        #region Fields
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        private readonly IPlaylistService playlistService;
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

        public ObservableCollection<Track> QueueCollection => playlistService.QueuePlaylist;

        #endregion

        public QueueViewModel(IAudioService audioService, IIconManager iconManager, IPlaylistService playlistService)
        {
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.playlistService = playlistService;

            this.audioService.IconChanged += OnIconChanged;
            this.audioService.TrackChanged += OnTrackChanged;

            this.playlistService.QueuePlaylistChanged += OnQueueCollectionChanged;

            this.audioService.SelectedTrack = QueueCollection[0];
        }

        private void OnQueueCollectionChanged()
        {
            OnPropertyChanged(nameof(QueueCollection));
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
                OnPropertyChanged(nameof(IconManager));
            }
        }

        public override void Dispose()
        {
            audioService.IconChanged -= OnIconChanged;
            audioService.TrackChanged -= OnTrackChanged;
            playlistService.QueuePlaylistChanged -= OnQueueCollectionChanged;
            base.Dispose();
        }
    }
}

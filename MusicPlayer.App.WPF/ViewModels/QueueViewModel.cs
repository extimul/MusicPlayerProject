using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

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
        public DrawingBrush PlayPauseIcon => iconManager.PlayPauseIcon;

        #endregion

        #region Commands
        public ICommand PlayPauseCommand { get; set; }
        #endregion

        public QueueViewModel(IAudioService audioService, IIconManager iconManager, IPlaylistService playlistService)
        {
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.playlistService = playlistService;

            this.audioService.IconChanged += OnIconChanged;
            this.audioService.TrackChanged += OnTrackChanged;

            this.audioService.ActivePlaylist = QueueCollection;
            this.playlistService.QueuePlaylistChanged += OnQueueCollectionChanged;
            this.audioService.SelectedTrack = QueueCollection[0];

            PlayPauseCommand = new PlayerControlsCommand(audioService, this);
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
                OnPropertyChanged(nameof(PlayPauseIcon));
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

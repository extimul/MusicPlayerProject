using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class AudioPlayerBarViewModel : ViewModelBase
    {
        #region Fields
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;

        public double trackVolumeValue;
        #endregion

        #region Properties
        public Track CurrentTrack => audioService.SelectedTrack;
        public TimeSpan TrackTimeValue => audioService.TrackTimePosition;
        public TimeSpan TrackDuration => audioService.TrackDuration;
        public long TrackPosition
        {
            get => audioService.TrackPosition;
            set
            {
                audioService.TrackPosition = value;
                OnPropertyChanged(nameof(TrackPosition));
            }
        }
        public long TrackLenght => audioService.TrackLenght; 
        public bool CanPlay => audioService.CanPlay;
        public double TrackVolumeValue
        {
            get => audioService.TrackVolumeValue;
            set
            {
                audioService.TrackVolumeValue = value;
                OnPropertyChanged(nameof(TrackVolumeValue));
            }
        }

        public DrawingBrush PlayPauseIcon => iconManager.PlayPauseIcon;
        public DrawingBrush TrackVolumeIcon => iconManager.VolumeIcon;
        #endregion

        #region Commands
        public ICommand AudioPlayerControlCommand { get; }

        #endregion

        public AudioPlayerBarViewModel(IAudioService audioService, IIconManager iconManager)
        {
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.audioService.TrackChanged += OnTrackChanged;
            this.audioService.IconChanged += OnIconChanged;
            this.audioService.VolumeChanged += OnVolumeChanged;
            this.audioService.TrackPositionChanged += OnTrackPositionChanged;

            AudioPlayerControlCommand = new PlayerControlsCommand(this);

            OnIconChanged(this, new ChangeIconEventArgs(SourceTypes.TogglePlaybackSource, this.audioService.CurrentPlaybackState));
            OnIconChanged(this, new ChangeIconEventArgs(SourceTypes.VolumeSource, this.audioService.TrackVolumeValue));
        }

        private void OnTrackPositionChanged()
        {
            OnPropertyChanged(nameof(TrackTimeValue));
            OnPropertyChanged(nameof(TrackPosition));
        }

        private void OnVolumeChanged()
        {
            OnPropertyChanged(nameof(TrackVolumeValue));
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(CanPlay));
            OnPropertyChanged(nameof(CurrentTrack));
            OnPropertyChanged(nameof(TrackLenght));
            OnPropertyChanged(nameof(TrackDuration));
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            if (e?.SourceState is SourceTypes.TogglePlaybackSource)
            {
                iconManager.PlayPauseIcon = iconManager.SetPlayPauseIcon((PlaybackState)e.Value);
                OnPropertyChanged(nameof(PlayPauseIcon));
            }
            else if (e?.SourceState is SourceTypes.VolumeSource)
            {
                iconManager.VolumeIcon = iconManager.SetVolumeIcon((double)e.Value);
                OnPropertyChanged(nameof(TrackVolumeIcon));
            }
        }

        public Task TogglePlayPause()
        {
            audioService.TogglePlayPause();
            return Task.CompletedTask;
        }

        public Task PreviousTrack()
        {
            audioService.PreviousTrack();
            return Task.CompletedTask;
        }

        public Task NextTrack()
        {
            audioService.NextTrack();
            return Task.CompletedTask;
        }
        public Task SetAsLikedTrack()
        {
            throw new NotImplementedException();
        }

        public Task ShuffleTracks()
        {
            throw new NotImplementedException();
        }

        public Task RepeatTrack()
        {
            throw new NotImplementedException();
        }

        public static AudioPlayerBarViewModel LoadMusicControlBarViewModel(IAudioService audioManager, IIconManager iconManager) => new(audioManager, iconManager);

        public override void Dispose()
        {
            audioService.IconChanged -= OnIconChanged;
            audioService.TrackChanged -= OnTrackChanged;
            audioService.TrackPositionChanged -= OnTrackPositionChanged;
            audioService.VolumeChanged -= OnVolumeChanged;
            base.Dispose();
        }
    }
}

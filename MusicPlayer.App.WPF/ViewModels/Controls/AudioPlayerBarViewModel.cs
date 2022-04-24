using System;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayer.App.WPF.Commands;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.Services.Audio;
using MusicPlayer.Core.Services.Icon;
using MusicPlayer.Core.Types;
using NAudio.Wave;

namespace MusicPlayer.App.WPF.ViewModels.Controls
{
    public sealed class AudioPlayerBarViewModel : ViewModelBase
    {
        #region Fields
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        public double trackVolumeValue;
        #endregion

        #region Properties
        public Track PlayingTrack => audioService.PlayingTrack;
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

            AudioPlayerControlCommand = new PlayerControlsCommand(audioService, this);

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
            OnPropertyChanged(nameof(PlayingTrack));
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

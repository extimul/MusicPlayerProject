using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using NAudio.Wave;

namespace MusicPlayerProject.ViewModels
{
    public class AudioPlayerBarViewModel : ViewModelBase
    {
        #region Properties
        public IAudioManager AudioManager { get; set; }
        public bool CanPlay => AudioManager.CanPlay;

        public DrawingBrush PlayPauseIcon { get; set; }

        public DrawingBrush VolumeIcon { get; set; }

        public ICommand AudioPlayerControlCommand { get; }

        #endregion

        public AudioPlayerBarViewModel(IAudioManager audioManager)
        {
            AudioManager = audioManager;
            AudioPlayerControlCommand = new PlayerControlsCommand(this);
            AudioManager.StateChanged += OnStateChanged;
            AudioManager.IconChanged += OnIconChanged;

            OnIconChanged(new ChangeIconEventArgs() 
            {
                SourceState = SourceTypes.TogglePlaybackSource,
                Value = AudioManager.CurrentPlaybackState
            });

            OnIconChanged(new ChangeIconEventArgs()
            {
                SourceState = SourceTypes.VolumeSource,
                Value = AudioManager.TrackVolumeValue
            });
        }

        private void OnIconChanged(ChangeIconEventArgs sender)
        {
            if (sender?.SourceState is SourceTypes.TogglePlaybackSource)
            {
                var state = (PlaybackState)sender?.Value;
                switch (state)
                {
                    case PlaybackState.Stopped:
                        PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];
                        break;
                    case PlaybackState.Playing:
                        PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icons.PauseIcon.ToString()];
                        break;
                    case PlaybackState.Paused:
                        PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];
                        break;
                }

                OnPropertyChanged(nameof(PlayPauseIcon));
            }
            else if(sender?.SourceState is SourceTypes.VolumeSource)
            {
                double volumeValue = (double)sender?.Value;

                if (volumeValue == 0)
                {
                    ChangeVolumeIcon(VolumeLevels.Mute);
                }
                else if (volumeValue is > 0 and <= 30.0)
                {
                    ChangeVolumeIcon(VolumeLevels.Low);
                }
                else if (volumeValue is > 30.0 and <= 65.0)
                {
                    ChangeVolumeIcon(VolumeLevels.Medium);
                }
                else if (volumeValue is > 65.0 and <= 100.0)
                {
                    ChangeVolumeIcon(VolumeLevels.High);
                }
                
                OnPropertyChanged(nameof(VolumeIcon));
            }
        }

        private void ChangeVolumeIcon(VolumeLevels volumeLevel)
        {
            VolumeIcon = volumeLevel switch
            {
                VolumeLevels.Mute => (DrawingBrush)Application.Current.Resources[Icons.VolumeOffIcon.ToString()],
                VolumeLevels.Low => (DrawingBrush)Application.Current.Resources[Icons.VolumeLowIcon.ToString()],
                VolumeLevels.Medium => (DrawingBrush)Application.Current.Resources[Icons.VolumeMediumIcon.ToString()],
                VolumeLevels.High => (DrawingBrush)Application.Current.Resources[Icons.VolumeHighIcon.ToString()],
                _ => VolumeIcon
            };
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(AudioManager));
            OnPropertyChanged(nameof(CanPlay));
        }

        public static AudioPlayerBarViewModel LoadMusicControlBarViewModel(IAudioManager audioManager)
        {
            AudioPlayerBarViewModel audioPlayerBarViewModel = new AudioPlayerBarViewModel(audioManager);

            return audioPlayerBarViewModel;
        }

        public override void Dispose()
        {
            AudioManager.StateChanged -= OnStateChanged;
            AudioManager.IconChanged -= OnIconChanged;
            base.Dispose();
        }
    }
}

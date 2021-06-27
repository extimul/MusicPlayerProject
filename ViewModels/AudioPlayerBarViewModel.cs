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
                PlayPauseIcon = state switch
                {
                    PlaybackState.Stopped => (DrawingBrush) Application.Current.Resources[Icons.PlayIcon.ToString()],
                    PlaybackState.Playing => (DrawingBrush) Application.Current.Resources[Icons.PauseIcon.ToString()],
                    PlaybackState.Paused => (DrawingBrush) Application.Current.Resources[Icons.PlayIcon.ToString()],
                    _ => PlayPauseIcon
                };

                OnPropertyChanged(nameof(PlayPauseIcon));
            }
            else if(sender?.SourceState is SourceTypes.VolumeSource)
            {
                double volumeValue = (double)sender?.Value;

                switch (volumeValue)
                {
                    case 0:
                        ChangeVolumeIcon(VolumeLevels.Mute);
                        break;
                    case > 0 and <= 30.0:
                        ChangeVolumeIcon(VolumeLevels.Low);
                        break;
                    case > 30.0 and <= 65.0:
                        ChangeVolumeIcon(VolumeLevels.Medium);
                        break;
                    case > 65.0 and <= 100.0:
                        ChangeVolumeIcon(VolumeLevels.High);
                        break;
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

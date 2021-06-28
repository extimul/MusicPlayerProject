using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Helpers;
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
                PlayPauseIcon = IconChanger.SetPlayPauseIcon(sender.Value);
                OnPropertyChanged(nameof(PlayPauseIcon));
            }
            else if(sender?.SourceState is SourceTypes.VolumeSource)
            {
                VolumeIcon = IconChanger.SetVolumeIcon(sender.Value);
                OnPropertyChanged(nameof(VolumeIcon));
            }
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

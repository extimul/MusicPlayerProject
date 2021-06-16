﻿using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MusicPlayerProject.Core.Commands
{
    public class PlayerControlsCommand : AsyncCommandBase
    {
        private readonly AudioPlayerBarViewModel _viewModel;

        public IAudioManager _audioManager; 

        public PlayerControlsCommand(IAudioManager audioManager, AudioPlayerBarViewModel playerBarViewModel)
        {
            _audioManager = audioManager;
            _viewModel = playerBarViewModel;

            _viewModel.PropertyChanged += AudioPlayerBarViewModel_PropertyChanged;
        }

        /// <summary>
        /// Check tracks in playlist
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanPlay && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is not null)
            {
                AudioPlayerControlType control = (AudioPlayerControlType)parameter;

                switch (control)
                {
                    case AudioPlayerControlType.StartPause:
                        StartPauseTrack();
                        break;
                    case AudioPlayerControlType.Next:
                        NextTrack();
                        break;
                    case AudioPlayerControlType.Previous:
                        PreviousTrack();
                        break;
                    case AudioPlayerControlType.Shuffle:
                        ShuffleTracks();
                        break;
                    case AudioPlayerControlType.Repeat:
                        RepeatTrack();
                        break;
                    case AudioPlayerControlType.Volume:
                        ChangeVolumeLevel();
                        break;
                    case AudioPlayerControlType.VolumeOff:
                        if (_viewModel.CurrentVolumeValue == 0)
                        {
                            _viewModel.CurrentVolumeValue = _viewModel.PreviousVolumeValue;
                        }
                        else
                        {
                            _viewModel.PreviousVolumeValue = _viewModel.CurrentVolumeValue;
                            _viewModel.CurrentVolumeValue = 0;
                        }
                        ChangeVolumeLevel();
                        break;
                    case AudioPlayerControlType.Favourite:
                        SetInFavouriteTrack();
                        break;
                }
            }
        }

        #region MainButtons
        #region StartPauseButton
        private void StartPauseTrack()
        {
            if (_viewModel.IsPlaying is false)
            {
                ChangePlayPauseIcon(true, Icon.PauseIcon);
            }
            else
            {
                ChangePlayPauseIcon(false, Icon.PlayIcon);
            }
        }

        private void ChangePlayPauseIcon(bool musicState, Icon iconKey)
        {
            _viewModel.IsPlaying = musicState;
            _viewModel.PlayPauseIcon = (DrawingBrush)Application.Current.Resources[iconKey.ToString()];
        }

        #endregion

        #region NextPreviousTrackButtons
        private void NextTrack()
        {
            MessageBox.Show("NextTrack");
        }

        private void PreviousTrack()
        {
            MessageBox.Show("PreviousTrack");
        }

        #endregion
        #endregion

        #region AdditionalButtons

        private void ShuffleTracks()
        {

        }

        private void RepeatTrack()
        {

        }

        private void SetInFavouriteTrack()
        {

        }

        #region Volume
        private void ChangeVolumeLevel()
        {
            if (_viewModel.CurrentVolumeValue == 0)
            {
                ChangeVolumeIcon(VolumeLevel.Mute);
            }
            else if (_viewModel.CurrentVolumeValue is > 0 and <= 30)
            {
                ChangeVolumeIcon(VolumeLevel.Low);
            }
            else if (_viewModel.CurrentVolumeValue is > 30 and <= 65)
            {
                ChangeVolumeIcon(VolumeLevel.Medium);
            }
            else if (_viewModel.CurrentVolumeValue is > 65 and <= 100)
            {
                ChangeVolumeIcon(VolumeLevel.High);
            }
        }

        private void ChangeVolumeIcon(VolumeLevel volumeLevel)
        {
            _viewModel.VolumeIcon = volumeLevel switch
            {
                VolumeLevel.Mute => (DrawingBrush) Application.Current.Resources[Icon.VolumeOffIcon.ToString()],
                VolumeLevel.Low => (DrawingBrush) Application.Current.Resources[Icon.VolumeLowIcon.ToString()],
                VolumeLevel.Medium => (DrawingBrush) Application.Current.Resources[Icon.VolumeMediumIcon.ToString()],
                VolumeLevel.High => (DrawingBrush) Application.Current.Resources[Icon.VolumeHighIcon.ToString()],
                _ => _viewModel.VolumeIcon
            };
        }

        #endregion

        #endregion

        private void AudioPlayerBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AudioPlayerBarViewModel.CanPlay))
            {
                OnCanExecuteChanged();
            }
        }
    }
}

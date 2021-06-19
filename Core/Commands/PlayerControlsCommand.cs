using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.ViewModels;
using NAudio.Wave;
using System;
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
                AudioPlayerControlTypes control = (AudioPlayerControlTypes)parameter;

                switch (control)
                {
                    case AudioPlayerControlTypes.StartPause:
                        StartPauseTrack();
                        break;
                    case AudioPlayerControlTypes.Next:
                        NextTrack();
                        break;
                    case AudioPlayerControlTypes.Previous:
                        PreviousTrack();
                        break;
                    case AudioPlayerControlTypes.Shuffle:
                        ShuffleTracks();
                        break;
                    case AudioPlayerControlTypes.Repeat:
                        RepeatTrack();
                        break;
                    case AudioPlayerControlTypes.Volume:
                        ChangeVolumeLevel();
                        break;
                    case AudioPlayerControlTypes.VolumeOff:
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
                    case AudioPlayerControlTypes.Favourite:
                        SetInFavouriteTrack();
                        break;
                }
            }
        }

        #region MainButtons
        #region StartPauseButton
        private void StartPauseTrack()
        {
            if (_audioManager.CurrentlyPlaybackState is PlaybackState.Stopped || _audioManager.CurrentlyPlaybackState is PlaybackState.Paused)
            {
                _audioManager.TogglePlayPauseTrack();
                _audioManager.PlaybackStopType = PlaybackStopTypes.ReachingEndOfFile;
                _viewModel.CurrentTrackLenght = _audioManager.TrackLenght;
                _audioManager.PlaybackPaused += AudioManager_PlaybackPaused;
                _audioManager.PlaybackResumed += AudioManager_PlaybackResumed;
                _audioManager.PlaybackStopped += AudioManager_PlaybackStopped;
            }
            else
            {
                _audioManager.PauseTrack();
                _audioManager.PlaybackStopType = PlaybackStopTypes.StoppedByUser;
                ChangePlayPauseIcon(PlaybackState.Paused, Icons.PlayIcon);
            }
        }

        private void AudioManager_PlaybackStopped()
        {
            ChangePlayPauseIcon(PlaybackState.Stopped, Icons.PlayIcon);
            _viewModel.CurrentTrackPosition = 0;

            if (_audioManager.PlaybackStopType is PlaybackStopTypes.ReachingEndOfFile)
            {
                _audioManager.NextTrack();
                StartPauseTrack();
            }
            else if (_audioManager.PlaybackStopType is PlaybackStopTypes.StoppedByUser)
            {
                StartPauseTrack();
            }
        }

        private void AudioManager_PlaybackResumed()
        {
            ChangePlayPauseIcon(PlaybackState.Playing, Icons.PauseIcon);
        }

        private void AudioManager_PlaybackPaused()
        {
            ChangePlayPauseIcon(PlaybackState.Paused, Icons.PlayIcon);
        }

        private void ChangePlayPauseIcon(PlaybackState musicState, Icons iconKey)
        {
            _audioManager.CurrentlyPlaybackState = musicState;
            _viewModel.PlayPauseIcon = (DrawingBrush)Application.Current.Resources[iconKey.ToString()];
        }

        #endregion

        #region NextPreviousTrackButtons
        private void NextTrack()
        {
            _audioManager.NextTrack();
        }

        private void PreviousTrack()
        {
            _audioManager.PreviousTrack();
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
            if (_viewModel.CurrentVolumeValue == 0.0)
            {
                ChangeVolumeIcon(VolumeLevels.Mute);
            }
            else if (_viewModel.CurrentVolumeValue is > 0.0 and <= 0.3)
            {
                ChangeVolumeIcon(VolumeLevels.Low);
            }
            else if (_viewModel.CurrentVolumeValue is > 0.3 and <= 0.65)
            {
                ChangeVolumeIcon(VolumeLevels.Medium);
            }
            else if (_viewModel.CurrentVolumeValue is > 0.65 and <= 1)
            {
                ChangeVolumeIcon(VolumeLevels.High);
            }
        }

        private void ChangeVolumeIcon(VolumeLevels volumeLevel)
        {
            _viewModel.VolumeIcon = volumeLevel switch
            {
                VolumeLevels.Mute => (DrawingBrush) Application.Current.Resources[Icons.VolumeOffIcon.ToString()],
                VolumeLevels.Low => (DrawingBrush) Application.Current.Resources[Icons.VolumeLowIcon.ToString()],
                VolumeLevels.Medium => (DrawingBrush) Application.Current.Resources[Icons.VolumeMediumIcon.ToString()],
                VolumeLevels.High => (DrawingBrush) Application.Current.Resources[Icons.VolumeHighIcon.ToString()],
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

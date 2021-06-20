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
                        await StartPauseTrack();
                        break;
                    case AudioPlayerControlTypes.Next:
                        _audioManager.NextTrack();
                        break;
                    case AudioPlayerControlTypes.Previous:
                        _audioManager.PreviousTrack();
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
                        await ChangeVolumeLevel();
                        break;
                    case AudioPlayerControlTypes.Favourite:
                        SetInFavouriteTrack();
                        break;
                }
            }
        }

        private async Task StartPauseTrack()
        {
            await Task.Run(() =>
            {
                if (_audioManager.CurrentlyPlaybackState is PlaybackState.Playing)
                {
                    _audioManager.PauseTrack();
                    ChangePlayPauseIcon(PlaybackState.Paused, Icons.PlayIcon);
                }
                else if (_audioManager.CurrentlyPlaybackState is PlaybackState.Paused)
                {
                    _audioManager.PlayTrack();
                    ChangePlayPauseIcon(PlaybackState.Playing, Icons.PauseIcon);
                }
                else if (_audioManager.CurrentlyPlaybackState is PlaybackState.Stopped)
                {
                    _audioManager.PlayTrack();
                    ChangePlayPauseIcon(PlaybackState.Playing, Icons.PauseIcon);
                }
            });
        }

        private void ChangePlayPauseIcon(PlaybackState musicState, Icons iconKey)
        {
            _audioManager.CurrentlyPlaybackState = musicState;
            _viewModel.PlayPauseIcon = (DrawingBrush)Application.Current.Resources[iconKey.ToString()];
        }

        private void ShuffleTracks()
        {
            throw new NotImplementedException();
        }

        private void RepeatTrack()
        {
            throw new NotImplementedException();
        }

        private void SetInFavouriteTrack()
        {
            throw new NotImplementedException();
        }

        private async Task ChangeVolumeLevel()
        {
            await Task.Run(() =>
            {
                if (_viewModel.CurrentVolumeValue == 0)
                {
                    ChangeVolumeIcon(VolumeLevels.Mute);
                }
                else if (_viewModel.CurrentVolumeValue is > 0 and <= 30.0)
                {
                    ChangeVolumeIcon(VolumeLevels.Low);
                }
                else if (_viewModel.CurrentVolumeValue is > 30.0 and <= 65.0)
                {
                    ChangeVolumeIcon(VolumeLevels.Medium);
                }
                else if (_viewModel.CurrentVolumeValue is > 65.0 and <= 100.0)
                {
                    ChangeVolumeIcon(VolumeLevels.High);
                }
            });
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

        private void AudioPlayerBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AudioPlayerBarViewModel.CanPlay))
            {
                OnCanExecuteChanged();
            }
        }
    }
}

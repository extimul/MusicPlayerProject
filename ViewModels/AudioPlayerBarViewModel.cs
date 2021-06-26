using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
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
        private readonly IAudioManager _audioManager;

        private double _previousVolumeValue;

        #region Properties

        public Track CurrentTrack => _audioManager.CurrentlySelectedTrack;

        public bool CanPlay => _audioManager.HasTracksInPlaylist;

        public double CurrentTrackLenght => _audioManager.TrackLenght;

        public TimeSpan CurrentTrackDuration => _audioManager.TrackDuration;

        public TimeSpan CurrentTrackTimePosition => _audioManager.TrackTimePosition;

        public double CurrentTrackPosition
        {
            get { return _audioManager.TrackPosition; }
            set
            {
                if (value.Equals(_audioManager.TrackPosition)) return;
                else
                {
                    _audioManager.TrackPosition = value;
                    OnPropertyChanged(nameof(CurrentTrackPosition));
                }
            }
        }

        #region AdditionalButtonsData

        public double PreviousVolumeValue
        {
            get { return _previousVolumeValue; }
            set
            {
                _previousVolumeValue = value;
                OnPropertyChanged(nameof(PreviousVolumeValue));
            }
        }

        public double CurrentVolumeValue
        {
            get { return _audioManager.TrackVolume; }
            set 
            {
                _audioManager.TrackVolume = value;
                AudioPlayerControlCommand?.Execute(AudioPlayerControlTypes.Volume);
                OnPropertyChanged(nameof(CurrentVolumeValue));
            }
        }

        #endregion

        #region IconSources

        private DrawingBrush _playPauseIconSource;

        private DrawingBrush _volumeIcon;
        public DrawingBrush PlayPauseIcon
        {
            get => _playPauseIconSource;
            set
            {
                _playPauseIconSource = value;
                OnPropertyChanged(nameof(PlayPauseIcon));
            }
        }

        public DrawingBrush VolumeIcon
        {
            get => _volumeIcon;
            set
            {
                _volumeIcon = value;
                OnPropertyChanged(nameof(VolumeIcon));
            }
        }

        #endregion

        public ICommand AudioPlayerControlCommand { get; }

        #endregion

        public AudioPlayerBarViewModel(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            AudioPlayerControlCommand = new PlayerControlsCommand(audioManager, this);

            CurrentVolumeValue = 50.0f;

            _audioManager.CurrentlyPlaybackState = PlaybackState.Stopped;

            PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];
            
            _audioManager.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(CurrentTrack));
            OnPropertyChanged(nameof(CurrentTrackTimePosition));
            OnPropertyChanged(nameof(CurrentTrackPosition));
            OnPropertyChanged(nameof(CurrentTrackLenght));
            OnPropertyChanged(nameof(CurrentTrackDuration));
        }

        public static AudioPlayerBarViewModel LoadMusicControlBarViewModel(IAudioManager audioManager)
        {
            AudioPlayerBarViewModel audioPlayerBarViewModel = new AudioPlayerBarViewModel(audioManager);

            return audioPlayerBarViewModel;
        }

        public override void Dispose()
        {
            _audioManager.StateChanged -= OnStateChanged;
            base.Dispose();
        }
    }
}

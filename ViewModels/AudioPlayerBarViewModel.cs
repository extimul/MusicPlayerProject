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
        private readonly IAudioManager _audioManager;

        private double _previousVolumeValue;

        #region Properties

        public Track CurrentTrack => _audioManager.CurrentlySelectedTrack;

        public bool CanPlay => _audioManager.HasTracksInPlaylist;

        public double CurrentTrackLenght => _audioManager.TrackLenght;

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
                if (Equals(value, _audioManager.TrackVolume)) return;
                else
                {
                    _audioManager.TrackVolume = value;
                    AudioPlayerControlCommand?.Execute(AudioPlayerControlTypes.Volume);
                    OnPropertyChanged(nameof(CurrentVolumeValue));
                }
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
            _audioManager.StateChanged += OnStateChanged;
            AudioPlayerControlCommand = new PlayerControlsCommand(audioManager, this);

            CurrentVolumeValue = 0.5;

            _audioManager.CurrentlyPlaybackState = PlaybackState.Stopped;

            PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];

            var timer = new System.Timers.Timer();
            timer.Interval = 300;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(CurrentTrack));
            OnPropertyChanged(nameof(CurrentTrackLenght));
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_audioManager.CurrentlyPlaybackState is PlaybackState.Playing)
            {
                CurrentTrackPosition = _audioManager.TrackPosition;
            }
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

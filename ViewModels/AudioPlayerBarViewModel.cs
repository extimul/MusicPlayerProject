using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.ViewModels.Base;
using NAudio.Wave;

namespace MusicPlayerProject.ViewModels
{
    public class AudioPlayerBarViewModel : ViewModelBase
    {
        private readonly IAudioManager _audioManager;
        public bool CanPlay => _audioManager.HasTracksInPlaylist;

        private double _previousVolumeValue;

        #region Properties

        public double CurrentTrackLenght
        {
            get { return _audioManager.TrackLenght; }
            set
            {
                if (value.Equals(_audioManager.TrackLenght)) return;
                OnPropertyChanged(nameof(CurrentTrackLenght));
            }
        }

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

        public ICommand SliderControlCommand { get; }

        #endregion

        public AudioPlayerBarViewModel(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            AudioPlayerControlCommand = new PlayerControlsCommand(audioManager, this);
            SliderControlCommand = new PlayerSliderControlCommand(audioManager, this);

            CurrentVolumeValue = 0.5;

            PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];

            var timer = new System.Timers.Timer();
            timer.Interval = 300;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
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
            base.Dispose();
        }
    }
}

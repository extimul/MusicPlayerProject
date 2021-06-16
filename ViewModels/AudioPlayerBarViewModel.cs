using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayerProject.ViewModels
{
    public class AudioPlayerBarViewModel : ViewModelBase
    {
        private readonly IAudioManager _audioManager;

        #region Properties
        public bool CanPlay => _audioManager.HasTracksInPlayList();

        private bool _isPlaying = false;

        private int _previousVolumeValue;

        private int _currentVolumeValue;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set 
            { 
                _isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }

        #region AdditionalButtonsData

        public int PreviousVolumeValue
        {
            get { return _previousVolumeValue; }
            set
            {
                _previousVolumeValue = value;
                OnPropertyChanged(nameof(PreviousVolumeValue));
            }
        }

        public int CurrentVolumeValue
        {
            get { return _currentVolumeValue; }
            set 
            {
                if (_currentVolumeValue != value)
                {
                    _currentVolumeValue = value;
                    AudioPlayerControlCommand?.Execute(AudioPlayerControlType.Volume);
                }
                
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
            AudioPlayerControlCommand = new PlayerControlsCommand(audioManager, this);

            CurrentVolumeValue = 50;

            PlayPauseIcon = (DrawingBrush)Application.Current.Resources[Icon.PlayIcon.ToString()];

            _audioManager = audioManager;
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

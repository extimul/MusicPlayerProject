using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayerProject.ViewModels
{
    public class AudioPlayerBarViewModel : ViewModelBase
    {
        private readonly IAudioManager _audioManager;

        #region Properties

        private bool _isPlaying = true;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set 
            { 
                _isPlaying = value;
                OnPropertyChanged(nameof(IsPlaying));
            }
        }

        private DrawingBrush _playPauseIconSource;

        public DrawingBrush PlayPauseIconSource
        {
            get { return _playPauseIconSource; }
            set
            {
                _playPauseIconSource = value;
                OnPropertyChanged(nameof(PlayPauseIconSource));
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            { 
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand MusicPlayerControlCommand { get; }

        #endregion

        public AudioPlayerBarViewModel(IAudioManager audioManager)
        {
            PlayPauseIconSource = (DrawingBrush)Application.Current.Resources["PlayIcon"];

            _audioManager = audioManager;

            MusicPlayerControlCommand = new PlayerControlsCommand(audioManager, this);
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

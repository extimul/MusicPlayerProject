using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicPlayerProject.Core.Commands
{
    public class PlayerControlsCommand : AsyncCommandBase
    {
        private readonly AudioPlayerBarViewModel _playerBarViewModel;

        public IAudioManager _audioManager; 

        public PlayerControlsCommand(IAudioManager audioManager, AudioPlayerBarViewModel playerBarViewModel)
        {
            _audioManager = audioManager;
            _playerBarViewModel = playerBarViewModel;

            _playerBarViewModel.PropertyChanged += AudioPlayerBarViewModel_PropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is not null)
            {
                AudioPlayerControlType control = (AudioPlayerControlType)parameter;

                switch (control)
                {
                    case AudioPlayerControlType.StartPause:
                        if (_playerBarViewModel.IsPlaying is true)
                        {
                            _playerBarViewModel.IsPlaying = false;
                            _playerBarViewModel.PlayPauseIconSource = (System.Windows.Media.DrawingBrush)Application.Current.Resources["PauseIcon"];
                        }
                        else
                        {
                            _playerBarViewModel.IsPlaying = true;
                            _playerBarViewModel.PlayPauseIconSource = (System.Windows.Media.DrawingBrush)Application.Current.Resources["PlayIcon"];
                        }
                        break;
                    case AudioPlayerControlType.Next:
                        break;
                    case AudioPlayerControlType.Previous:
                        break;
                    default:
                        break;
                }
            }
        }

        private void AudioPlayerBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
    }
}

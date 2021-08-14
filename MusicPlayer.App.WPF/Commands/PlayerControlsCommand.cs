using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.Core.Types;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class PlayerControlsCommand : AsyncCommandBase
    {
        private AudioPlayerBarViewModel _viewModel;

        public PlayerControlsCommand(AudioPlayerBarViewModel playerBarViewModel)
        {
            _viewModel = playerBarViewModel;
            _viewModel.PropertyChanged += AudioPlayerBarViewModel_PropertyChanged;
        }

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
                        await _viewModel.AudioManager.TogglePlayPause();
                        break;
                    case AudioPlayerControlTypes.Next:
                        await _viewModel.AudioManager.NextTrack();
                        break;
                    case AudioPlayerControlTypes.Previous:
                        await _viewModel.AudioManager.PreviousTrack();
                        break;
                    case AudioPlayerControlTypes.Shuffle:
                        await _viewModel.AudioManager.ShuffleTracks();
                        break;
                    case AudioPlayerControlTypes.Repeat:
                        await _viewModel.AudioManager.RepeatTrack();
                        break;
                    case AudioPlayerControlTypes.VolumeOff:
                        _viewModel.AudioManager.TrackVolumeValue = 0;
                        break;
                    case AudioPlayerControlTypes.IsLiked:
                        await _viewModel.AudioManager.SetAsLikedTrack();
                        break;
                    case AudioPlayerControlTypes.Volume:
                        break;
                }
            }
        }

        private void AudioPlayerBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanPlay))
            {
                OnCanExecuteChanged();
            }
        }
    }
}

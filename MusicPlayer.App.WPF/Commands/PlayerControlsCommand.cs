using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Types;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class PlayerControlsCommand : AsyncCommandBase
    {
        private readonly IAudioService audioService;
        private readonly ViewModelBase viewModel;

        public PlayerControlsCommand(IAudioService audioService, ViewModelBase viewModel) 
        {
            this.audioService = audioService;
            this.viewModel = viewModel;
            this.viewModel.PropertyChanged += AudioPlayerBarViewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return audioService.CanPlay && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AudioPlayerControlTypes control = (AudioPlayerControlTypes)parameter;

            switch (control)
            {
                case AudioPlayerControlTypes.StartPause:
                    await audioService.TogglePlayPause();
                    break;
                case AudioPlayerControlTypes.Next:
                    await audioService.NextTrack();
                    break;
                case AudioPlayerControlTypes.Previous:
                    await audioService.PreviousTrack();
                    break;
                case AudioPlayerControlTypes.Shuffle:
                    await audioService.ShuffleTracks();
                    break;
                case AudioPlayerControlTypes.Repeat:
                    await audioService.RepeatTrack();
                    break;
                case AudioPlayerControlTypes.VolumeOff:
                    audioService.TrackVolumeValue = 0;
                    break;
                case AudioPlayerControlTypes.DoubleClickSwitch:
                    await audioService.StopTrack();
                    await audioService.PlayTrack();
                    break;
            }
        }

        private void AudioPlayerBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(audioService.CanPlay))
            {
                OnCanExecuteChanged();
            }
        }
    }
}

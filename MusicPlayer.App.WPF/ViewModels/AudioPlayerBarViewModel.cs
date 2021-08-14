using System.Windows.Input;
using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class AudioPlayerBarViewModel : ViewModelBase
    {
        #region Properties
        public IAudioManager AudioManager { get; set; }
        public IIconManager IconManager { get; set; }
        public bool CanPlay => AudioManager.CanPlay;
        public ICommand AudioPlayerControlCommand { get; }
        #endregion

        public AudioPlayerBarViewModel(IAudioManager audioManager, IIconManager iconManager)
        {
            AudioManager = audioManager;
            IconManager = iconManager;
            AudioPlayerControlCommand = new PlayerControlsCommand(this);
            AudioManager.StateChanged += OnStateChanged;
            AudioManager.IconChanged += OnIconChanged;

            OnIconChanged(this, new ChangeIconEventArgs(SourceTypes.TogglePlaybackSource, AudioManager.CurrentPlaybackState));
            OnIconChanged(this, new ChangeIconEventArgs(SourceTypes.VolumeSource, AudioManager.TrackVolumeValue));
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            if (e?.SourceState is SourceTypes.TogglePlaybackSource)
            {
                IconManager.PlayPauseIcon = IconManager.SetPlayPauseIcon((PlaybackState)e.Value);
            }
            else if (e?.SourceState is SourceTypes.VolumeSource)
            {
                IconManager.VolumeIcon = IconManager.SetVolumeIcon((double)e.Value);
            }
            OnPropertyChanged(nameof(IconManager));
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(AudioManager));
            OnPropertyChanged(nameof(CanPlay));
        }

        public static AudioPlayerBarViewModel LoadMusicControlBarViewModel(IAudioManager audioManager, IIconManager iconManager) => new(audioManager, iconManager);

        public override void Dispose()
        {
            AudioManager.StateChanged -= OnStateChanged;
            AudioManager.IconChanged -= OnIconChanged;
            base.Dispose();
        }
    }
}

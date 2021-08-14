using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class QueueViewModel : ViewModelBase
    {
        private readonly IPlaylistService _playlistManager;
        #region Properties
        public IAudioService AudioManager { get; set; }
        public IIconManager IconManager { get; }
        #endregion

        public QueueViewModel(IAudioService audioManager, IPlaylistService playlistManager, IIconManager iconManager)
        {
            _playlistManager = playlistManager;
            IconManager = iconManager;
            AudioManager = audioManager;
            LoadQueueCollection();
            AudioManager.StateChanged += OnStateChanged;
            AudioManager.IconChanged += OnIconChanged;
        }

        private void LoadQueueCollection()
        {
            if (_playlistManager != null)
            {
                
            }

            AudioManager.SelectedTrack = AudioManager.LoadedPlaylist[0];
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            if (e.SourceState is SourceTypes.TogglePlaybackSource)
            {
                IconManager.PlayPauseIcon = IconManager.SetPlayPauseIcon((PlaybackState)e.Value);
                OnPropertyChanged(nameof(IconManager));
            }
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(AudioManager));
        }

        public override void Dispose()
        {
            AudioManager.StateChanged -= OnStateChanged;
            AudioManager.IconChanged -= OnIconChanged;
            base.Dispose();
        }
    }
}

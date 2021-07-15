using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Managers.Icon;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using NAudio.Wave;

namespace MusicPlayerProject.ViewModels
{
    public class QueueViewModel : ViewModelBase
    {
        private readonly IPlaylistManager _playlistManager;
        #region Properties
        public IAudioManager AudioManager { get; set; }
        public IIconManager IconManager { get; }
        #endregion

        public QueueViewModel(IAudioManager audioManager, IPlaylistManager playlistManager, IIconManager iconManager)
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
            if (e.SourceState is Core.Enums.SourceTypes.TogglePlaybackSource)
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

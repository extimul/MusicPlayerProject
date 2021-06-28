using MusicPlayerProject.Core.Helpers;
using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System;
using System.Windows.Media;

namespace MusicPlayerProject.ViewModels
{
    public class QueueViewModel : ViewModelBase
    {
        private readonly IPlaylistManager _playlistManager;
        #region Properties

        public IAudioManager AudioManager { get; set; }

        public DrawingBrush PlayPauseIcon { get; set; }

        #endregion

        public QueueViewModel(IAudioManager audioManager, IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
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

        private void OnIconChanged(ChangeIconEventArgs sender)
        {
            if (sender.SourceState is Core.Enums.SourceTypes.TogglePlaybackSource)
            {
                PlayPauseIcon = IconChanger.SetPlayPauseIcon(sender.Value);
                OnPropertyChanged(nameof(PlayPauseIcon));
            }
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(AudioManager));
        }

        public override void Dispose()
        {
            AudioManager.StateChanged -= OnStateChanged;
            base.Dispose();
        }
    }
}

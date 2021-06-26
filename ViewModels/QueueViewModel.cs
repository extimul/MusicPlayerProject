using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System;
using System.Collections.ObjectModel;

namespace MusicPlayerProject.ViewModels
{
    public class QueueViewModel : ViewModelBase
    {
        #region Properties

        public IAudioManager AudioManager { get; set; }

        #endregion

        public QueueViewModel(IAudioManager audioManager)
        {
            AudioManager = audioManager;
            AudioManager.SelectedTrack = AudioManager.LoadedPlaylist[0];
            AudioManager.StateChanged += OnStateChanged;
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

using MusicPlayerProject.Core.Managers.Audio;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System;
using System.Collections.ObjectModel;

namespace MusicPlayerProject.ViewModels
{
    public class QueueViewModel : ViewModelBase
    {
        #region private fields
        private readonly IAudioManager _audioManager;

        #endregion

        #region Properties

        public ObservableCollection<Track> QueueCollection
        {
            get => _audioManager.LoadedPlaylist;
        }

        public Track SelectedTrack
        {
            get => _audioManager.CurrentlySelectedTrack;
            set 
            {
                if (value.Equals(_audioManager.CurrentlySelectedTrack)) return;
                else
                {
                    _audioManager.CurrentlySelectedTrack = value;
                    OnPropertyChanged(nameof(SelectedTrack));
                }
            }
        }

        #endregion

        public QueueViewModel(IAudioManager audioManager)
        {
            _audioManager = audioManager;

            _audioManager.StateChanged += OnStateChanged;

            SelectedTrack = _audioManager.LoadedPlaylist[0];
        }

        private void OnStateChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
        }
    }
}

using System;
using System.Collections.ObjectModel;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Models;
using NAudio.Wave;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public interface IAudioManager
    {
        #region Events
        event Action StateChanged;
        event EventHandler<ChangeIconEventArgs> IconChanged;
        #endregion

        #region Properties
        public PlaybackState CurrentPlaybackState { get; }
        public PlaybackStopTypes PlaybackStopType { get; set; }
        public double TrackVolumeValue { get; set; }
        public long TrackLenght { get; }
        public long TrackPosition { get; set; }
        public TimeSpan TrackDuration { get; }
        public TimeSpan TrackTimePosition { get; }
        public Track PlayingTrack { get; set; }
        public Track SelectedTrack { get; set; }
        public ObservableCollection<Track> LoadedPlaylist { get; set; }
        public bool HasTracksInPlaylist { get; }
        public bool CanPlay { get; }
        #endregion

        #region Audio manager control methods
        void TogglePlayPause();
        void PlayTrack();
        void PauseTrack();
        void StopTrack();
        void NextTrack();
        void PreviousTrack();
        void ShuffleTracks();
        void RepeatTrack();
        void SetAsLikedTrack();
        #endregion
    }
}

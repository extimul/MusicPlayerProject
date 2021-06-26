using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Models;
using NAudio.Wave;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public interface IAudioManager
    {
        #region events
        event Action StateChanged;

        event Action PlaybackResumed;

        event Action PlaybackPaused;

        event Action PlaybackStopped;

        #endregion

        #region Properties
        public PlaybackState CurrentlyPlaybackState { get; set; }
        public PlaybackStopTypes PlaybackStopType { get; set; }
        public double TrackVolume { get; set; }
        public double TrackLenght { get; }
        public double TrackPosition { get; set; }
        public TimeSpan TrackDuration { get; }
        public TimeSpan TrackTimePosition { get; }
        public bool HasTracksInPlaylist { get; }
        public Track CurrentlyPlayingTrack { get; set; }
        public Track CurrentlySelectedTrack { get; set; }
        public ObservableCollection<Track> LoadedPlaylist { get; set; }
        #endregion

        #region Audio manager control methods
        Task PlayTrack();
        Task PauseTrack();
        Task StopTrack();
        Task NextTrack();
        Task PreviousTrack();

        #endregion
    }
}

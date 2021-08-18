using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;

namespace MusicPlayer.App.WPF.Services.Audio
{
    public interface IAudioService
    {
        #region Events
        event Action TrackChanged;
        event Action VolumeChanged;
        event Action TrackPositionChanged;
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
        public ObservableCollection<Track> ActivePlaylist { get; set; }
        public bool CanPlay { get; }
        #endregion

        #region Audio manager control methods
        Task TogglePlayPause();
        Task PlayTrack();
        Task PauseTrack();
        Task StopTrack();
        Task NextTrack();
        Task PreviousTrack();
        Task ShuffleTracks();
        Task RepeatTrack();
        Task SetAsLikedTrack();
        #endregion
    }
}

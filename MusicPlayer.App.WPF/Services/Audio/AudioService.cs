using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Settings;
using MusicPlayer.Core.Exceptions;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;

namespace MusicPlayer.App.WPF.Services.Audio
{
    public class AudioService : IAudioService
    {
        #region Events
        public event Action TrackChanged;
        public event Action VolumeChanged;
        public event Action TrackPositionChanged;
        public event EventHandler<ChangeIconEventArgs> IconChanged;
        #endregion

        #region Fields
        private readonly IDataPathService dataPathService;
        private readonly DispatcherTimer _timer;
        private WaveStream _audioFileReader;
        private IWavePlayer _wavePlayer;

        private ObservableCollection<Track> _loadedPlaylist;
        private Track _currentlyPlayingTrack;
        private Track _currentlySelectedTrack;
        private long _trackPosition;
        private double _lastTrackVolumeValue;
        private TimeSpan _trackTimePosition;
        private PlaybackStopTypes _playbackStopType;
        #endregion

        #region Properties

        public Track PlayingTrack
        {
            get => _currentlyPlayingTrack;
            set
            {
                if (value.Equals(_currentlyPlayingTrack)) return;
                _currentlyPlayingTrack = value;
            }
        }

        public Track SelectedTrack
        {
            get => _currentlySelectedTrack;
            set
            {
                if (value.Equals(_currentlySelectedTrack)) return;
                _currentlySelectedTrack = value;
                TrackChanged.Invoke();
            }
        }

        public ObservableCollection<Track> LoadedPlaylist
        {
            get => _loadedPlaylist;
            set
            {
                if (value.Equals(_loadedPlaylist)) return;
                _loadedPlaylist = value;
            }
        }

        public PlaybackState CurrentPlaybackState => _wavePlayer?.PlaybackState ?? PlaybackState.Stopped;

        public PlaybackStopTypes PlaybackStopType
        {
            get => _playbackStopType;
            set
            {
                if (value.Equals(_playbackStopType)) return;
                _playbackStopType = _audioFileReader is null ? PlaybackStopTypes.StoppedByUser : value;
            }
        }
        public double TrackVolumeValue
        {
            get => _wavePlayer?.Volume * 100.0f ?? 50.0f;
            set
            {
                if (_wavePlayer == null) return;
                switch (value)
                {
                    case 0 when _wavePlayer.Volume == 0:
                        _wavePlayer.Volume = (float)_lastTrackVolumeValue / 100.0f;
                        break;
                    case 0 when _wavePlayer.Volume != 0 && _lastTrackVolumeValue != 0:
                        _wavePlayer.Volume = 0;
                        break;
                    default:
                        _lastTrackVolumeValue = TrackVolumeValue;
                        _wavePlayer.Volume = (float)value / 100.0f;
                        break;
                }

                VolumeChanged?.Invoke();
                IconChanged?.Invoke(this, new ChangeIconEventArgs(SourceTypes.VolumeSource, (double)_wavePlayer.Volume * 100.0f));
            }
        }

        public long TrackLenght => _audioFileReader?.Length ?? 0;

        public long TrackPosition
        {
            get => _trackPosition;
            set
            {
                if (_audioFileReader == null) return;
                if (value.Equals(_trackPosition)) return;
                _trackPosition = value;
                _audioFileReader.Position = _trackPosition;
                TrackPositionChanged?.Invoke();
            }
        }

        public TimeSpan TrackDuration => _audioFileReader?.TotalTime ?? TimeSpan.FromSeconds(0);

        public TimeSpan TrackTimePosition
        {
            get => _audioFileReader?.CurrentTime ?? TimeSpan.FromSeconds(0);
            set
            {
                if (value.Equals(_trackTimePosition)) return;
                _trackTimePosition = value;
                TrackPositionChanged?.Invoke();
            }
        }

        public bool CanPlay => SelectedTrack != null;
        #endregion

        public AudioService()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += new EventHandler(TimerOnTick);
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            if (_audioFileReader == null) return;
            _trackPosition = _audioFileReader.Position;
            _trackTimePosition = _audioFileReader?.CurrentTime ?? TimeSpan.FromSeconds(0);
            TrackPositionChanged?.Invoke();
        }

        public Task TogglePlayPause()
        {
            try
            {
                switch (CurrentPlaybackState)
                {
                    case PlaybackState.Stopped:
                        PlayTrack();
                        break;
                    case PlaybackState.Playing:
                        PauseTrack();
                        break;
                    case PlaybackState.Paused:
                        PlayTrack();
                        break;
                }
            }
            catch (TrackNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Task.CompletedTask;
        }

        public Task PlayTrack()
        {
            if (!SelectedTrack.TrackIsExsist)
            {
                throw new TrackNotFoundException(SelectedTrack.TrackTitle);
            }

            if (SelectedTrack != null && SelectedTrack != PlayingTrack)
            {
                PlayingTrack = SelectedTrack;
            }

            if (_wavePlayer is null)
            {
                _wavePlayer = new WaveOutEvent();
                _wavePlayer.PlaybackStopped += OnPlaybackStopped;
            }

            if (_audioFileReader is null)
            {
                TrackVolumeValue = 50;

                _audioFileReader = new AudioFileReader(PlayingTrack.TrackSource)
                {
                    Volume = (float)TrackVolumeValue / 100.0f
                };

                TrackPosition = 0;

                TrackTimePosition = TimeSpan.FromSeconds(0);

                PlaybackStopType = PlaybackStopTypes.StoppedByUser;

                _wavePlayer.Init(_audioFileReader);
            }

            _wavePlayer?.Play();
            _timer.Start();

            TrackChanged?.Invoke();
            TrackPositionChanged?.Invoke();
            VolumeChanged?.Invoke();
            IconChanged?.Invoke(this, new ChangeIconEventArgs(SourceTypes.TogglePlaybackSource, CurrentPlaybackState));

            return Task.CompletedTask;
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (_audioFileReader != null && _audioFileReader.Position == TrackLenght)
                PlaybackStopType = PlaybackStopTypes.ReachingEndOfFile;

            if (PlaybackStopType is PlaybackStopTypes.ReachingEndOfFile)
                NextTrack();
            else
                StopTrack();
        }

        public Task PauseTrack()
        {
            _wavePlayer?.Pause();
            IconChanged?.Invoke(this, new ChangeIconEventArgs(SourceTypes.TogglePlaybackSource, CurrentPlaybackState));
            _timer.Stop();
            return Task.CompletedTask;
        }

        public Task StopTrack()
        {
            _wavePlayer?.Dispose();
            _wavePlayer = null;
            _audioFileReader?.Dispose();
            _audioFileReader = null;
            _timer?.Stop();
            IconChanged?.Invoke(this, new ChangeIconEventArgs(SourceTypes.TogglePlaybackSource, CurrentPlaybackState));
            return Task.CompletedTask;
        }

        public Task NextTrack()
        {
            if (CanPlay && SelectedTrack.GetId() < LoadedPlaylist.Count - 1)
            {
                StopTrack();
                SelectedTrack = LoadedPlaylist[SelectedTrack.GetId() + 1];
                PlayTrack();
            }
            else
            {
                StopTrack();
                SelectedTrack = LoadedPlaylist[0];
                PlayTrack();
            }
            return Task.CompletedTask;
        }

        public Task PreviousTrack()
        {
            if (CanPlay && SelectedTrack.GetId() > 0)
            {
                StopTrack();
                SelectedTrack = LoadedPlaylist[SelectedTrack.GetId() - 1];
                PlayTrack();
            }
            else
            {
                StopTrack();
                SelectedTrack = LoadedPlaylist[^1];
                PlayTrack();
            }
            return Task.CompletedTask;
        }

        public Task ShuffleTracks()
        {
            throw new NotImplementedException();
        }

        public Task RepeatTrack()
        {
            throw new NotImplementedException();
        }

        public Task SetAsLikedTrack()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Models;
using NAudio.Wave;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public class AudioManager : IAudioManager
    {
        #region Events
        public event Action StateChanged;
        public event IAudioManager.IconChangeHandler IconChanged;
        #endregion

        #region Fields
        private DispatcherTimer _timer;
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
                else
                {
                    _currentlyPlayingTrack = value;
                    StateChanged?.Invoke();
                }
            }
        }
        public Track SelectedTrack
        {
            get => _currentlySelectedTrack;
            set
            {
                if (value.Equals(_currentlySelectedTrack)) return;
                else
                {
                    _currentlySelectedTrack = value;
                    StateChanged?.Invoke();
                }
            }
        }
        public ObservableCollection<Track> LoadedPlaylist
        {
            get => _loadedPlaylist;
            set
            {
                if (value.Equals(_loadedPlaylist)) return;
                else
                {
                    _loadedPlaylist = value;
                    StateChanged?.Invoke();
                }
            }
        }
        public PlaybackState CurrentPlaybackState
        {
            get => (_wavePlayer != null) ? _wavePlayer.PlaybackState : PlaybackState.Stopped;
        }
        public PlaybackStopTypes PlaybackStopType
        {
            get => _playbackStopType;
            set
            {
                if (value.Equals(_playbackStopType)) return;
                else
                {
                    if (_audioFileReader is null) _playbackStopType = PlaybackStopTypes.StoppedByUser;
                    else _playbackStopType = value;
                }
            }
        }
        public double TrackVolumeValue
        {
            get { return (_wavePlayer != null) ? _wavePlayer.Volume * 100.0f : 50.0f; }
            set
            {
                if (_wavePlayer != null)
                {
                    if (value == 0 && _wavePlayer.Volume == 0)
                    {
                        _wavePlayer.Volume = (float)_lastTrackVolumeValue / 100.0f;
                    }
                    else if (value == 0 && _wavePlayer.Volume != 0 && _lastTrackVolumeValue != 0)
                    {
                        _wavePlayer.Volume = 0;
                    }
                    else
                    {
                        _lastTrackVolumeValue = TrackVolumeValue;
                        _wavePlayer.Volume = (float)value / 100.0f;
                    }

                    IconChanged?.Invoke(new ChangeIconEventArgs()
                    {
                        SourceState = SourceTypes.VolumeSource,
                        Value = value
                    });
                    StateChanged?.Invoke();
                }

            }
        }
        public long TrackLenght => (_audioFileReader != null) ? _audioFileReader.Length : 0;
        public long TrackPosition
        {
            get => _trackPosition;
            set
            {
                if (_audioFileReader != null)
                {
                    if (value.Equals(_trackPosition)) return;
                    else
                    {
                        _trackPosition = value;
                        _audioFileReader.Position = _trackPosition;
                        StateChanged?.Invoke();
                    }
                }
            }
        }
        public TimeSpan TrackDuration
        {
            get => (_audioFileReader != null) ? _audioFileReader.TotalTime : TimeSpan.FromSeconds(0);
        }
        public TimeSpan TrackTimePosition
        {
            get => (_audioFileReader != null) ? _audioFileReader.CurrentTime : TimeSpan.FromSeconds(0);
            set
            {
                if (value.Equals(_trackTimePosition)) return;
                else
                {
                    _trackTimePosition = value;
                    StateChanged?.Invoke();
                }
            }
        }
        public bool HasTracksInPlaylist => LoadedPlaylist.Count > 0;
        public bool CanPlay => HasTracksInPlaylist && SelectedTrack != null;


        #endregion

        public AudioManager()
        {
            LoadedPlaylist = new ObservableCollection<Track>()
            {
                new Track()
                {
                    Id = 1,
                    TrackTitle = "Жизнь поёт",
                    TrackAlbum = "Album",
                    Author = "Monatik",
                    IsLiked = true,
                    TrackImage = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\DefaultSongImg.png",
                    TrackSource = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\track.mp3",
                    Duration = TimeSpan.FromSeconds(1000)
                },
                new Track()
                {
                    Id = 2,
                    TrackTitle = "Spirits",
                    TrackAlbum = "Album",
                    Author = "Kabes",
                    IsLiked = true,
                    TrackImage = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\DefaultSongImg.png",
                    TrackSource = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\track2.mp3",
                    Duration = TimeSpan.FromSeconds(300)
                },
                new Track()
                {
                    Id = 3,
                    TrackTitle = "Spirits",
                    TrackAlbum = "Album",
                    Author = "Kabes",
                    IsLiked = true,
                    TrackImage = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\DefaultSongImg.png",
                    TrackSource = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\track.mp3",
                    Duration = TimeSpan.FromSeconds(300)
                },
                new Track()
                {
                    Id = 4,
                    TrackTitle = "Spirits",
                    TrackAlbum = "Album",
                    Author = "Kabes",
                    IsLiked = true,
                    TrackImage = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\DefaultSongImg.png",
                    TrackSource = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\track.mp3",
                    Duration = TimeSpan.FromSeconds(300)
                }
            };
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += new EventHandler(TimerOnTick);
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            if (_audioFileReader != null)
            {
                _trackPosition = _audioFileReader.Position;
                _trackTimePosition = (_audioFileReader != null) ? _audioFileReader.CurrentTime : TimeSpan.FromSeconds(0);
                StateChanged?.Invoke();
            }
        }

        public void TogglePlayPause()
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

        public void PlayTrack()
        {
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
                TrackVolumeValue = 50.0;

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

            StateChanged?.Invoke();
            IconChanged?.Invoke(new ChangeIconEventArgs()
            {
                SourceState = SourceTypes.TogglePlaybackSource,
                Value = CurrentPlaybackState
            });
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

        public void PauseTrack()
        {
            _wavePlayer?.Pause();
            IconChanged?.Invoke(new ChangeIconEventArgs()
            {
                SourceState = SourceTypes.TogglePlaybackSource,
                Value = CurrentPlaybackState
            });
            _timer.Stop();
        }

        public void StopTrack()
        {
            _wavePlayer?.Dispose();
            _wavePlayer = null;
            _audioFileReader?.Dispose();
            _audioFileReader = null;
            _timer?.Stop();
            StateChanged?.Invoke();
            IconChanged?.Invoke(new ChangeIconEventArgs()
            {
                SourceState = SourceTypes.TogglePlaybackSource,
                Value = CurrentPlaybackState
            });
        }

        public void NextTrack()
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
        }

        public void PreviousTrack()
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
                SelectedTrack = LoadedPlaylist[LoadedPlaylist.Count - 1];
                PlayTrack();
            }
        }

        public void ShuffleTracks()
        {
            throw new NotImplementedException();
        }

        public void RepeatTrack()
        {
            throw new NotImplementedException();
        }

        public void SetAsLikedTrack()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Models;
using NAudio.Wave;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public class AudioManager : IAudioManager
    {
        #region Fields
        private DispatcherTimer _timer;

        private WaveStream _audioFileReader;

        private IWavePlayer _wavePlayer;

        private Track _currentlyPlayingTrack;

        private Track _currentlySelectedTrack;

        private PlaybackState _currentlyPlaybackState;

        private PlaybackStopTypes _playbackStopType;

        private ObservableCollection<Track> _loadedPlaylist;

        private double _trackPosition;

        private const double SliderMax = 10.0;

        #region Events

        public event Action StateChanged;
        public event Action PlaybackResumed;
        public event Action PlaybackPaused;
        public event Action PlaybackStopped;

        #endregion

        #endregion

        #region Properties

        public Track CurrentlyPlayingTrack
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

        public Track CurrentlySelectedTrack
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

        public bool HasTracksInPlaylist => LoadedPlaylist.Count > 0;

        public PlaybackState CurrentlyPlaybackState
        {
            get => _currentlyPlaybackState;
            set
            {
                if (value.Equals(_currentlyPlaybackState)) return;
                else
                {
                    _currentlyPlaybackState = value;
                    StateChanged?.Invoke();
                }
            }
        }

        public PlaybackStopTypes PlaybackStopType
        {
            get => _playbackStopType;
            set
            {
                if (value.Equals(_playbackStopType)) return;
                else
                {
                    _playbackStopType = value;
                    StateChanged?.Invoke();
                }
            }
        }

        public double TrackVolume
        {
            get { return (_wavePlayer != null) ? _wavePlayer.Volume * 100.0f : 50.0f; }
            set
            {
                if (_wavePlayer != null)
                {
                    if (value.Equals(_wavePlayer.Volume)) return;
                    else
                    {
                        _wavePlayer.Volume = (float)value / 100.0f;
                        StateChanged?.Invoke();
                    }
                }

            }
        }

        public double TrackLenght => SliderMax;

        public double TrackPosition
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
                        long pos = (long)(_audioFileReader.Position * _trackPosition / SliderMax);
                        _audioFileReader.Position = pos;
                        StateChanged?.Invoke();
                    }
                }
            }
        }

        public TimeSpan TrackDuration
        {
            get => (_audioFileReader != null) ? _audioFileReader.TotalTime : TimeSpan.FromSeconds(0);
            set
            {
                StateChanged?.Invoke();
            }
        }
        public TimeSpan TrackTimePosition
        {
            get => (_audioFileReader != null) ? _audioFileReader.CurrentTime : TimeSpan.FromSeconds(0);
            set
            {
                StateChanged.Invoke();
            }
        }
 
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
                _trackPosition = Math.Min(SliderMax, _audioFileReader.Position * SliderMax / _audioFileReader.Length);
                StateChanged?.Invoke();
            }
        }

        public async Task PlayTrack()
        {
            await Task.Run(() =>
            {
                if (CurrentlySelectedTrack != null && CurrentlySelectedTrack != CurrentlyPlayingTrack)
                {
                    CurrentlyPlayingTrack = CurrentlySelectedTrack;
                }

                if (_wavePlayer is null)
                {
                    _wavePlayer = new WaveOutEvent();
                    _wavePlayer.PlaybackStopped += OnPlaybackStopped;
                }

                if (_audioFileReader is null)
                {
                    _audioFileReader = new AudioFileReader(CurrentlyPlayingTrack.TrackSource)
                    {
                        Volume = (float)TrackVolume / 100.0f
                    };

                    _wavePlayer.Init(_audioFileReader);
                }

                _wavePlayer?.Play();
                _timer.Start();
            });
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            TrackPosition = 0;
            _wavePlayer.Dispose();
            _wavePlayer = null;
            _audioFileReader.Dispose();
            _audioFileReader = null;
            _timer.Stop();
            StateChanged?.Invoke();
        }

        public async Task PauseTrack()
        {
            await Task.Run(() =>
            {
                _wavePlayer?.Pause();
                _timer.Stop();
            });
        }

        public async Task StopTrack()
        {
            await Task.Run(() =>
            {
                _wavePlayer?.Stop();
                _timer.Stop();
            });
        }

        public async Task NextTrack()
        {
            if (HasTracksInPlaylist && CurrentlySelectedTrack.GetId() < LoadedPlaylist.Count - 1)
            {
                await StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[CurrentlySelectedTrack.GetId() + 1];
                await PlayTrack();
            }
            else
            {
                await StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[0];
                await PlayTrack();
            }
        }

        public async Task PreviousTrack()
        {
            if (HasTracksInPlaylist && CurrentlySelectedTrack.GetId() > 0)
            {
                await StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[CurrentlySelectedTrack.GetId() - 1];
                await PlayTrack();
            }
            else
            {
                await StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[LoadedPlaylist.Count - 1];
                await PlayTrack();
            }
        }
    }
}

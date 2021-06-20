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
        #region Fields
        private DispatcherTimer _timer;

        private AudioFileReader _audioFileReader;

        private WaveOutEvent _outputDevice;

        private Track _currentlyPlayingTrack;

        private Track _currentlySelectedTrack;

        private PlaybackState _currentlyPlaybackState;

        private PlaybackStopTypes _playbackStopType;

        private ObservableCollection<Track> _loadedPlaylist;

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
            get { return (_audioFileReader != null) ? _audioFileReader.Volume * 100.0f : 50.0f; }
            set
            {
                if (_audioFileReader != null)
                {
                    if (value.Equals(_audioFileReader.Volume)) return;
                    else
                    {
                        _audioFileReader.Volume = (float)value / 100.0f;
                        StateChanged?.Invoke();
                    }
                }

            }
        }

        public double TrackLenght => (_audioFileReader != null) ? _audioFileReader.TotalTime.TotalSeconds : 0;

        public double TrackPosition
        {
            get { return (_audioFileReader != null) ? _audioFileReader.CurrentTime.TotalSeconds : 0; }
            set
            {
                if (_audioFileReader != null)
                {
                    if (value.Equals(_audioFileReader.CurrentTime.TotalSeconds)) return;
                    else
                    {
                        _audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
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
            _timer.Tick += TimerOnTick;
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            StateChanged?.Invoke();
        }

        public void PlayTrack()
        {
            if (CurrentlySelectedTrack != null && CurrentlySelectedTrack != CurrentlyPlayingTrack)
            {
                CurrentlyPlayingTrack = CurrentlySelectedTrack;
            }

            if (_outputDevice is null)
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }

            if (_audioFileReader is null)
            {
                _audioFileReader = new AudioFileReader(CurrentlyPlayingTrack.TrackSource)
                {
                    Volume = (float)TrackVolume / 100.0f
                };

                _outputDevice.Init(_audioFileReader);
            }
            
            _outputDevice?.Play();
            _timer.Start();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _outputDevice.Dispose();
            _outputDevice = null;
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }

        public void PauseTrack()
        {
            _outputDevice?.Pause();
            _timer.Stop();
        }

        public void StopTrack()
        {
            _outputDevice?.Stop();
            _timer.Stop();
        }

        public void NextTrack()
        {
            if (HasTracksInPlaylist && CurrentlySelectedTrack.GetId() < LoadedPlaylist.Count - 1)
            {
                StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[CurrentlySelectedTrack.GetId() + 1];
                PlayTrack();
            }
            else
            {
                StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[0];
                PlayTrack();
            }
        }

        public void PreviousTrack()
        {
            if (HasTracksInPlaylist && CurrentlySelectedTrack.GetId() > 0)
            {
                StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[CurrentlySelectedTrack.GetId() - 1];
                PlayTrack();
            }
            else
            {
                StopTrack();
                CurrentlySelectedTrack = LoadedPlaylist[LoadedPlaylist.Count - 1];
                PlayTrack();
            }
        }
    }
}

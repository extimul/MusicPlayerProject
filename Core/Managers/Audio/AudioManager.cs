using System;
using System.Collections.ObjectModel;
using MusicPlayerProject.Core.Enums;
using MusicPlayerProject.Core.Models;
using NAudio.Wave;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public class AudioManager : IAudioManager
    {
        #region Fields

        private AudioFileReader _audioFileReader;

        private DirectSoundOut _outputDevice;

        private Track _currentlyPlayingTrack;

        private Track _currentlySelectedTrack;

        private PlaybackState _currentlyPlaybackState;

        private PlaybackStopTypes _playbackStopType;

        private ObservableCollection<Track> _loadedPlaylist;

        public event Action StateChanged;
        public event Action PlaybackResumed;
        public event Action PlaybackPaused;
        public event Action PlaybackStopped;

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
                    TrackSource = "E:\\Projects\\VisualStudioProjects\\MusicPlayerProject\\ApplicationResources\\track.mp3",
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
        }

        public void LoadAudioFile()
        {
            _audioFileReader = new AudioFileReader(CurrentlySelectedTrack.TrackSource)
            {
                Volume = (float)TrackVolume
            };

            _outputDevice = new DirectSoundOut(200);

            WaveChannel32 waveChannel = new WaveChannel32(_audioFileReader);
            waveChannel.PadWithZeroes = false;

            _outputDevice.Init(waveChannel);
        }

        public void TogglePlayPauseTrack()
        {
            throw new NotImplementedException();
        }

        public void PauseTrack()
        {
            throw new NotImplementedException();
        }

        public void StopTrack()
        {
            throw new NotImplementedException();
        }

        public void NextTrack()
        {
            throw new NotImplementedException();
        }

        public void PreviousTrack()
        {
            throw new NotImplementedException();
        }

        

        #region Methods

        //public void NextTrack()
        //{
        //    if (HasTracksInPlaylist&& CurrentlySelectedTrack.GetId() < LoadedPlaylist.Count - 1)
        //    {
        //        StopTrack();
        //        CurrentlySelectedTrack = LoadedPlaylist[CurrentlySelectedTrack.GetId() + 1];
        //    }
        //    else
        //    {
        //        StopTrack();
        //        CurrentlySelectedTrack = LoadedPlaylist[0];
        //    }
        //}

        //public void PreviousTrack()
        //{
        //    if (HasTracksInPlaylist&& CurrentlySelectedTrack.GetId() > 0)
        //    {
        //        StopTrack();
        //        CurrentlySelectedTrack = LoadedPlaylist[CurrentlySelectedTrack.GetId() - 1];
        //    }
        //    else
        //    {
        //        StopTrack();
        //        CurrentlySelectedTrack = LoadedPlaylist[LoadedPlaylist.Count - 1];
        //    }
        //}

        #endregion
    }
}

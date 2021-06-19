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

        private Track _currentlyPlayingTrack;

        private Track _currentlySelectedTrack;

        private ObservableCollection<Track> _loadedPlaylist;

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

        //public double TrackVolume
        //{
        //    get { return (_audioFileReader != null) ? _audioFileReader.Volume : 0; }
        //    set
        //    {
        //        if (_audioFileReader != null)
        //        {
        //            if (value.Equals(_audioFileReader.Volume)) return;
        //            else
        //            {
        //                _audioFileReader.Volume = (float)value;
        //                StateChanged?.Invoke();
        //            }
        //        }

        //    }
        //}

        //public double TrackLenght => (_audioFileReader != null) ? _audioFileReader.TotalTime.TotalSeconds : 0;

        //public double TrackPosition
        //{
        //    get { return (_audioFileReader != null) ? _audioFileReader.CurrentTime.TotalSeconds : 0; }
        //    set
        //    {
        //        if (_audioFileReader != null)
        //        {
        //            if (value.Equals(_audioFileReader.CurrentTime.TotalSeconds)) return;
        //            else
        //            {
        //                _audioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
        //                StateChanged?.Invoke();
        //            }
        //        }
        //    }
        //}

        public bool HasTracksInPlaylist => LoadedPlaylist.Count > 0;

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

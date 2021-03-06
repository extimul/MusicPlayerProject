using System;

namespace MusicPlayer.Core.Exceptions
{
    public sealed class TrackNotFoundException : Exception
    {
        public string TrackName { get; }
        public TrackNotFoundException()
        {
        }

        public TrackNotFoundException(string trackName) : base($"{trackName} not found, please change source")
        {
        }
    }
}

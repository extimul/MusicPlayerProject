using System;

namespace MusicPlayer.Core.Models
{
    public class Track : BaseModel
    {
        public string TrackTitle { get; set; }
        public string TrackAlbum { get; set; }
        public string Author { get; set; }
        public bool IsLiked { get; set; }
        public TimeSpan Duration { get; set; }
        public string TrackImage { get; set; }
        public string TrackSource { get; set; }
        public override int GetId()
        {
            return Id - 1;
        }
    }
}

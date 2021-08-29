using System.IO;

namespace MusicPlayer.Core.Models
{
    public sealed class Track : BaseMusicPlayerModel
    {
        public string FileName { get; set; }
        public string TrackAlbum { get; set; }
        public bool IsLiked { get; set; }
        public string TrackSource { get; set; }
        public bool TrackIsExsist => File.Exists(TrackSource);
        public static string GetFullTrackPath(string directory, string fileName) => Path.Combine(directory, fileName);
    }
}

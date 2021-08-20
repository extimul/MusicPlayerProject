using System;
using System.IO;

namespace MusicPlayer.Core.Models
{
    public class Track : BaseModel
    {
        #region Properties
        public string FileName { get; set; }
        public string TrackAlbum { get; set; }
        public string Author { get; set; }
        public bool IsLiked { get; set; }
        public TimeSpan Duration { get; set; }
        public string TrackImage { get; set; }
        public string TrackSource { get; set; }
        public bool TrackIsExsist => File.Exists(TrackSource);
        #endregion

        #region Methods
        public static string GetFullTrackPath(string directory, string fileName) => Path.Combine(directory, fileName);
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicPlayer.Core.Models
{
    public class Playlist : BaseModel
    {
        public string PlaylistName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Track> Tracks { get; set; } = new ObservableCollection<Track>();
        public string Author { get; set; }
        public string ImageSource { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? RecentlyPlay { get; set; }
        public override int GetId()
        {
            return Id - 1;
        }
    }
}

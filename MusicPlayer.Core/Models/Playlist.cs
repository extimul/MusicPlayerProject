using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicPlayer.Core.Models
{
    public class Playlist : BaseMusicPlayerModel
    {
        public IEnumerable<Track> Tracks { get; set; } = new ObservableCollection<Track>();
        public string Description { get; set; }
    }
}

using System.Collections.ObjectModel;

namespace MusicPlayer.Core.Models
{
    public class Playlist : BaseMusicPlayerModel
    {
        public ObservableCollection<Track> TracksCollection { get; set; }
        public string Description { get; set; }

        public Playlist()
        {
            TracksCollection = new ObservableCollection<Track>();
        }
    }
}

using System.Collections.ObjectModel;

namespace MusicPlayer.Core.Models
{
    public sealed class Queue
    {
        public ObservableCollection<Track> TracksCollection { get; set; }

        public Queue()
        {
            TracksCollection = new ObservableCollection<Track>();
        }
    }
}

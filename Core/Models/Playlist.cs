using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Models
{
    public class Playlist : BaseModel
    {
        public string PlaylistName { get; set; }
        public IEnumerable<Track> Tracks { get; set; } = new ObservableCollection<Track>();
        public string Author { get; set; }

        public override int GetId()
        {
            return Id - 1;
        }
    }
}

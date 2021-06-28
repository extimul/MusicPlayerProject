using MusicPlayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public interface IPlaylistManager
    {
        public IEnumerable<Track> SavedQueueCollection { get; set; }
        public void LoadQueue();
    }
}

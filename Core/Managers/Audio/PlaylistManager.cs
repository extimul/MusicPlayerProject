using MusicPlayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Managers.Audio
{
    public class PlaylistManager : IPlaylistManager
    {
        public IEnumerable<Track> SavedQueueCollection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void LoadQueue()
        {
            throw new NotImplementedException();
        }
    }
}

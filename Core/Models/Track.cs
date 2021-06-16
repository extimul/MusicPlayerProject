using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string TrackTitle { get; set; }
        public string AuthorName { get; set; }
        public string TrackSource { get; set; }
    }
}

using MusicPlayerProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerProject.Core.Commands
{
    public class SortPlaylistsCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is SortTypes)
            {
                SortTypes sortType = (SortTypes)parameter;

                switch (sortType)
                {
                    case SortTypes.MostRelevant:
                        Trace.WriteLine(sortType.ToString());
                        break;
                    case SortTypes.RecentlyPlayed:
                        Trace.WriteLine(sortType.ToString());
                        break;
                    case SortTypes.RecentlyAdded:
                        Trace.WriteLine(sortType.ToString());
                        break;
                    case SortTypes.Alphabetical:
                        Trace.WriteLine(sortType.ToString());
                        break;
                }
            }
        }
    }
}

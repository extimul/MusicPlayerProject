using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.Core.Types;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
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

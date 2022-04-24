using System;
using MusicPlayer.Core.MVVMBase.Commands;
using MusicPlayer.Core.Types;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class SortPlaylistsCommand : AsyncCommandBase
    {
        public override Task ExecuteAsync(object parameter)
        {
            if (parameter is not SortTypes sortType) return Task.CompletedTask;
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return Task.CompletedTask;
        }
    }
}

using MusicPlayer.Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.Core.Interfaces
{
    public interface ITracksListView
    {
        public ObservableCollection<Track> TracksCollection { get; }
        public ObservableCollection<MenuItemObject> ContextMenuItems { get; }
        public DrawingBrush PlayPauseIcon { get; }
        public ICommand PlayPauseCommand { get; set; }
        public Track SelectedTrack { get; set; }
    }
}

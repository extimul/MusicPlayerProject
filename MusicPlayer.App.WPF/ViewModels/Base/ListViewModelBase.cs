using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.ViewModels.Base
{
    public abstract class ListViewModelBase : ViewModelBase
    {
        public PlaylistControlBarViewModel ControlBarViewModel { get; set; }
        public abstract Track SelectedTrack { get; set; }
        public abstract int SelectedTrackIndex { get; set; }
        public abstract DrawingBrush PlayPauseIcon { get; }
        public abstract ObservableCollection<Track> TracksCollection { get; }
        public abstract ObservableCollection<MenuItemObject> ContextMenuItems { get; set; }
        public abstract ICommand PlayPauseCommand { get; set; }
        public abstract ICommand ContextMenuCommand { get; set; }
        public abstract void LoadContextMenuItems();
    }
}

using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.ViewModels.Base
{
    public abstract class CollectionViewModel : ViewModelBase
    {
        public PlaylistControlBarViewModel ControlBarViewModel { get; set; }

        public virtual Track SelectedTrack { get; set; }

        public virtual Playlist CurrentPlaylist { get; set; }

        public abstract DrawingBrush PlayPauseIcon { get; }

        public abstract ObservableCollection<Track> TracksCollection { get; }

        public abstract ObservableCollection<MenuItemObject> ContextMenuItems { get; set; }

        public abstract ICommand PlayPauseCommand { get; set; }
        public abstract ICommand ContextMenuCommand { get; set; }

        public abstract void LoadContextMenuItems();
    }
}

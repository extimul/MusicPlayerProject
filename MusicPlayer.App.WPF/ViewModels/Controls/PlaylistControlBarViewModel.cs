using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;

namespace MusicPlayer.App.WPF.ViewModels.Controls
{
    public sealed class PlaylistControlBarViewModel : ViewModelBase
    {
        #region Fields
        private readonly PlaylistViewModel listViewModel;
        #endregion

        #region Properties
        public FilterHandlerPanelViewModel<Track> FilterPanelViewModel { get; private set; }
        #endregion

        public PlaylistControlBarViewModel(PlaylistViewModel listViewModel)
        {
            this.listViewModel = listViewModel;
            FilterPanelViewModel = new FilterHandlerPanelViewModel<Track>(listViewModel.CurrentPlaylist.Tracks);
        }

        public override void Dispose()
        {
            FilterPanelViewModel.Dispose();
            base.Dispose();
        }
    }
}

using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using System.Windows.Input;

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

        #region Command
        public ICommand AddTrackCommand { get; set; }
        #endregion

        public PlaylistControlBarViewModel(PlaylistViewModel listViewModel,
                                           IDataPathService pathService,
                                           IContentManager<Track, Playlist> contentManager)
        {
            this.listViewModel = listViewModel;
            FilterPanelViewModel = new FilterHandlerPanelViewModel<Track>(listViewModel.CurrentPlaylist.TracksCollection);

            AddTrackCommand = new AddTrackCommand(pathService, contentManager);
        }

        public override void Dispose()
        {
            FilterPanelViewModel.Dispose();
            base.Dispose();
        }
    }
}

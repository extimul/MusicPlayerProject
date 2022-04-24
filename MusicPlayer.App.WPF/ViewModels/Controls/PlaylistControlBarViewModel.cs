using MusicPlayer.App.WPF.Commands;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.Services.Content;
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

            contentManager.LoadData(listViewModel.CurrentPlaylist);
            AddTrackCommand = new AddTrackCommand(pathService, contentManager);
        }

        public override void Dispose()
        {
            FilterPanelViewModel.Dispose();
            base.Dispose();
        }
    }
}

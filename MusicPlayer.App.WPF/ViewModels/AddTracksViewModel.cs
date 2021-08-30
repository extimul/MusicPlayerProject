using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Dialog;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using System;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.ViewModels
{
    public sealed class AddTracksViewModel : ViewModelBase, ICreatorDialog
    {
        private readonly IDataPathService pathService;
        private readonly IContentManager<Track, Playlist> contentManager;

        public event EventHandler<DialogCreateRequestArgs> CloseRequested;

        #region Commands
        public ICommand CancelCommand { get; }
        public ICommand AddTrackCommand { get; }
        #endregion

        public AddTracksViewModel(IDataPathService pathService,
                                  IContentManager<Track, Playlist> contentManager)
        {
            this.pathService = pathService;
            this.contentManager = contentManager;

            CancelCommand = new RelayCommand<object>(o => Cancel());
        }

        private void Cancel()
        {
            CloseRequested?.Invoke(null, new DialogCreateRequestArgs(null));
        }

        public override void Dispose()
        {
            CloseRequested = null;
            base.Dispose();
        }
    }
}

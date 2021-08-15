using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Dialog;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.Views.DialogWindows;
using MusicPlayer.Core.Helpers;
using MusicPlayer.Core.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer.App.WPF.Commands
{
    public class CreatePlaylistCommand : AsyncCommandBase
    {
        private readonly LibraryViewModel libraryViewModel;
        private readonly IDataPathService pathService;
        private readonly IPlaylistService playlistService;
        private CreatePlaylistViewModel viewModel;
        private CreatePlaylistDialog view;

        public CreatePlaylistCommand(LibraryViewModel libraryViewModel, IDataPathService pathService, IPlaylistService playlistService)
        {
            this.libraryViewModel = libraryViewModel;
            this.pathService = pathService;
            this.playlistService = playlistService;
        }

        private void OnCloseRequested(object sender, DialogCreateRequestArgs e)
        {
            if (e.Result != null)
            {
                playlistService.AddPlaylist((Playlist)e.Result);
                view?.Close();
            }
            else
            {
                view?.Close();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Window mainWindow = Application.Current.MainWindow;
            if (viewModel != null)
            {
                viewModel.CloseRequested -= OnCloseRequested;
                viewModel = null;
            }

            if (viewModel == null)
            {
                viewModel = new CreatePlaylistViewModel(libraryViewModel, pathService, playlistService);
                viewModel.CloseRequested += OnCloseRequested;
            }

            view = new CreatePlaylistDialog
            {
                DataContext = viewModel,
                Owner = mainWindow
            };

            BlurEffect.Apply(mainWindow, 10);
            view.ShowDialog();
            BlurEffect.Clear(mainWindow);
        }
    }
}
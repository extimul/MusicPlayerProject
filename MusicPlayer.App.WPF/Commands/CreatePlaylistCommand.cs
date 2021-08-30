using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Dialog;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.Views.DialogWindows;
using MusicPlayer.Core.Helpers;
using MusicPlayer.Core.Models;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer.App.WPF.Commands
{
    public class CreatePlaylistCommand : AsyncCommandBase
    {
        private readonly LibraryViewModel libraryViewModel;
        private readonly IDataPathService pathService;
        private readonly IContentManager<Playlist> contentManager;
        private CreatePlaylistViewModel viewModel;
        private CreatePlaylistDialog view;

        public CreatePlaylistCommand(LibraryViewModel libraryViewModel, 
                                     IDataPathService pathService,
                                     IContentManager<Playlist> contentManager)
        {
            this.libraryViewModel = libraryViewModel;
            this.pathService = pathService;
            this.contentManager = contentManager;
        }

        private async void OnCloseRequested(object sender, DialogCreateRequestArgs e)
        {
            if (e.Result != null)
            {
                await contentManager.Add((Playlist)e.Result);
                view?.Close();
            }
            else
            {
                view?.Close();
            }
        }

        public override Task ExecuteAsync(object parameter)
        {
            Window mainWindow = Application.Current.MainWindow;
            if (viewModel != null)
            {
                viewModel.CloseRequested -= OnCloseRequested;
                viewModel = null;
            }

            if (viewModel == null)
            {
                viewModel = new CreatePlaylistViewModel(libraryViewModel, pathService, contentManager);
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
            return Task.CompletedTask;
        }
    }
}
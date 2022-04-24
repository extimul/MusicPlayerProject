using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.Views.DialogWindows;
using MusicPlayer.Core.Helpers;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase.Commands;
using MusicPlayer.Core.Services.Content;
using MusicPlayer.Core.Services.Dialog;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer.App.WPF.Commands
{
    public class CreatePlaylistCommand : AsyncCommandBase
    {
        private readonly IDataPathService pathService;
        private readonly IContentManager<Playlist, Library> contentManager;
        private CreatePlaylistViewModel viewModel;
        private CreatePlaylistDialog view;

        public CreatePlaylistCommand(IDataPathService pathService,
                                     IContentManager<Playlist, Library> contentManager)
        {
            this.pathService = pathService;
            this.contentManager = contentManager;
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
                viewModel = new CreatePlaylistViewModel(pathService, contentManager);
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
    }
}
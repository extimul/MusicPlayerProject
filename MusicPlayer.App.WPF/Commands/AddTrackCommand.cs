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
    public class AddTrackCommand : AsyncCommandBase
    {
        private readonly IDataPathService pathService;
        private readonly IContentManager<Track, Playlist> contentManager;
        private AddTracksViewModel viewModel;
        private AddTracksDialog view;

        public AddTrackCommand(IDataPathService pathService, IContentManager<Track, Playlist> contentManager)
        {
            this.pathService = pathService;
            this.contentManager = contentManager;
        }

        public override Task ExecuteAsync(object parameter)
        {
            Window mainWindow = Application.Current.MainWindow;
            if (viewModel != null)
            {
                viewModel.CloseRequested -= OnCloseRequestedAsync;
                viewModel = null;
            }

            if (viewModel == null)
            {
                viewModel = new AddTracksViewModel(pathService, contentManager);
                viewModel.CloseRequested += OnCloseRequestedAsync;
            }

            view = new AddTracksDialog
            {
                DataContext = viewModel,
                Owner = mainWindow
            };

            BlurEffect.Apply(mainWindow, 10);
            view.ShowDialog();
            BlurEffect.Clear(mainWindow);
            return Task.CompletedTask;
        }

        private async void OnCloseRequestedAsync(object sender, DialogCreateRequestArgs e)
        {
            if (e.Result != null)
            {
                await contentManager.Add((Track)e.Result);
                view?.Close();
            }
            else
            {
                view?.Close();
            }
            Dispose();
        }

        public void Dispose()
        {
            viewModel.CloseRequested -= OnCloseRequestedAsync;
        }
    }
}

using MusicPlayerProject.Core.Commands.Base;
using MusicPlayerProject.Core.Helpers;
using MusicPlayerProject.Core.Managers.Dialog;
using MusicPlayerProject.ViewModels;
using MusicPlayerProject.Views.DialogWindows;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayerProject.Core.Commands
{
    public class CreatePlaylistCommand : AsyncCommandBase
    {
        private readonly LibraryViewModel _libraryView;
        private CreatePlaylistViewModel viewModel;
        private CreatePlaylistDialog view;

        public CreatePlaylistCommand(LibraryViewModel libraryView)
        {
            _libraryView = libraryView;
            
        }

        private void OnCloseRequested(object sender, DialogCreateRequestArgs e)
        {
            if (e.Result != null)
            {
                Trace.WriteLine(e.Result);
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
                viewModel = new CreatePlaylistViewModel();
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
using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.Services.Dialog;
using System;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Win32;
using MusicPlayer.Core.Models;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Audio;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class CreatePlaylistViewModel : ViewModelBase, ICreatorDialog
    {
        #region Events
        public event EventHandler<DialogCreateRequestArgs> CloseRequested;
        #endregion

        #region Fields
        private string playlistImageSource;
        private string playlistName;
        private string playlistDescription;
        private readonly LibraryViewModel libraryViewModel;
        private readonly IDataPathService pathService;
        private readonly IPlaylistService playlistService;
        #endregion

        #region Properties

        public string PlaylistImageSource
        {
            get => playlistImageSource;
            set => SetField(ref playlistImageSource, value);
        }

        public string PlaylistName
        {
            get => playlistName;
            set => SetField(ref playlistName, value);
        }

        public string PlaylistDescription
        {
            get => playlistDescription;
            set => SetField(ref playlistDescription, value);
        }

        #endregion

        #region Commands 
        public ICommand ChangeImageCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        public CreatePlaylistViewModel(LibraryViewModel libraryViewModel, IDataPathService pathService, IPlaylistService playlistService)
        {
            this.pathService = pathService;
            this.playlistService = playlistService;
            this.libraryViewModel = libraryViewModel;
            CreateCommand = new RelayCommand(() => CreatePlaylist());
            CancelCommand = new RelayCommand(() => Cancel());
            ChangeImageCommand = new RelayCommand(() => ChangeImageOnClick());
        }

        private void ChangeImageOnClick()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() is true)
                {
                    PlaylistImageSource = fileDialog.FileName;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreatePlaylist()
        {
            CloseRequested?.Invoke(this, new DialogCreateRequestArgs(new Playlist()
            {
                PlaylistName = PlaylistName ?? $"Playlist #{playlistService.PlaylistsCollection.Count + 1}",
                Description = PlaylistDescription ?? "Your playlist",
                ImageSource = PlaylistImageSource ?? pathService.DefaultTrackImage,
                AddedDate = DateTime.Now
            }));
        }

        private void Cancel()
        {
            CloseRequested?.Invoke(this, new DialogCreateRequestArgs(null));
        }

        public override void Dispose()
        {
            CloseRequested = null;
            base.Dispose();
        }
    }
}

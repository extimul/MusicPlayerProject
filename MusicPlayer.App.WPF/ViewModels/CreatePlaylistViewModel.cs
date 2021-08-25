using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.Services.Dialog;
using System;
using System.Windows.Input;
using Microsoft.Win32;
using MusicPlayer.Core.Models;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;

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
        private readonly ITracksCollectionService<Playlist> tracksCollectionService;
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

        #region Consturctor
        public CreatePlaylistViewModel(LibraryViewModel libraryViewModel, 
                                       IDataPathService pathService, 
                                       ITracksCollectionService<Playlist> tracksCollectionService)
        {
            this.pathService = pathService;
            this.tracksCollectionService = tracksCollectionService;
            this.libraryViewModel = libraryViewModel;
            CreateCommand = new RelayCommand<object>(o => CreatePlaylist());
            CancelCommand = new RelayCommand<object>(o => Cancel());
            ChangeImageCommand = new RelayCommand<object>(o => ChangeImageOnClick(o));
        }
        #endregion

        #region Methods
        private void ChangeImageOnClick(object param)
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
                Title = PlaylistName ?? $"Playlist #{tracksCollectionService.TracksCollection.Count + 1}",
                Description = PlaylistDescription ?? "Your playlist",
                ImageSource = PlaylistImageSource ?? pathService.DefaultTrackImagePath,
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
        #endregion
    }
}

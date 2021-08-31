using System;
using System.Windows.Input;
using Microsoft.Win32;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Services.Content;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.Services.Dialog;
using MusicPlayer.Core.MVVMBase.Commands;

namespace MusicPlayer.App.WPF.ViewModels
{
    public sealed class CreatePlaylistViewModel : ViewModelBase, ICreatorDialog
    {
        #region Events
        public event EventHandler<DialogCreateRequestArgs> CloseRequested;
        #endregion

        #region Fields
        private string playlistImageSource;
        private string playlistName;
        private string playlistDescription;
        private readonly IDataPathService pathService;
        private readonly IContentManager<Playlist, Library> contentManager;
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
        public CreatePlaylistViewModel(IDataPathService pathService,
                                       IContentManager<Playlist, Library> contentManager)
        {
            this.pathService = pathService;
            this.contentManager = contentManager;
            CreateCommand = new RelayCommand<object>(o => CreatePlaylist());
            CancelCommand = new RelayCommand<object>(o => Cancel());
            ChangeImageCommand = new RelayCommand<object>(o => ChangeImageOnClick());
        }
        #endregion

        #region Methods
        private void ChangeImageOnClick()
        {
            try
            {
                OpenFileDialog fileDialog = new();
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
                Id = Guid.NewGuid(),
                Title = PlaylistName ?? $"Playlist #{contentManager.MusicModelsCollection.Count + 1}",
                Description = PlaylistDescription ?? "Your playlist",
                ImageSource = PlaylistImageSource ?? pathService.DefaultTrackImagePath,
                RecentlyPlay = DateTime.Now,
                AddedDate = DateTime.Now
            }));
        }

        private void Cancel()
        {
            //CloseRequested?.Invoke(this, new DialogCreateRequestArgs(null));
            CloseRequested?.Invoke(null, new DialogCreateRequestArgs(null));
        }

        public override void Dispose()
        {
            CloseRequested = null;
            base.Dispose();
        }
        #endregion
    }
}

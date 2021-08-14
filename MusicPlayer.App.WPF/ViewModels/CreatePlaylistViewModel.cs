using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.Services.Dialog;
using System;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Win32;
using MusicPlayer.Core.Models;
using MusicPlayer.App.WPF.Services.DataPath;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class CreatePlaylistViewModel : ViewModelBase, ICreatorDialog
    {
        #region Events
        public event EventHandler<DialogCreateRequestArgs> CloseRequested;
        #endregion

        #region Fields
        private string _playlistImageSource;
        private string _playlistName;
        private string _playlistDescription;
        private readonly LibraryViewModel _libraryViewModel;
        private readonly IDataPathService pathService;
        #endregion

        #region Properties

        public string PlaylistImageSource
        {
            get => _playlistImageSource;
            set
            {
                if (value.Equals(_playlistImageSource)) return;
                _playlistImageSource = value;
                OnPropertyChanged(nameof(PlaylistImageSource));
            }
        }

        public string PlaylistName
        {
            get => _playlistName;
            set
            {
                if (value.Equals(_playlistName)) return;
                _playlistName = value;
                OnPropertyChanged(nameof(PlaylistName));
            }
        }


        public string PlaylistDescription
        {
            get => _playlistDescription;
            set
            {
                if (value.Equals(_playlistDescription)) return;
                _playlistDescription = value;
                OnPropertyChanged(nameof(PlaylistDescription));
            }
        }


        #endregion

        #region Commands 
        public ICommand ChangeImageCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        public CreatePlaylistViewModel(LibraryViewModel libraryViewModel, IDataPathService pathService)
        {
            this.pathService = pathService;
            _libraryViewModel = libraryViewModel;
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
                PlaylistName = PlaylistName ?? $"Playlist #{_libraryViewModel.PlaylistManager.PlaylistsCollection.Count + 1}",
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

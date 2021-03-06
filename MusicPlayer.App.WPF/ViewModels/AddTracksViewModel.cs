using Microsoft.Win32;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase;
using MusicPlayer.Core.MVVMBase.Commands;
using MusicPlayer.Core.Services.Content;
using MusicPlayer.Core.Services.Dialog;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TagLib;

namespace MusicPlayer.App.WPF.ViewModels
{
    public sealed class AddTracksViewModel : ViewModelBase, ICreatorDialog
    {
        public event EventHandler<DialogCreateRequestArgs> CloseRequested;

        #region Fields
        private readonly IDataPathService pathService;
        private readonly IContentManager<Track, Playlist> contentManager;
        private Track selectedTrack;
        #endregion

        #region Commands
        public ICommand CancelCommand { get; }
        public ICommand AddTrackCommand { get; }
        public ICommand DeleteTrackCommand { get; }
        public ICommand ChangeImageCommand { get; }
        public ICommand SaveCommand { get; }
        #endregion

        #region Properties
        public ObservableCollection<Track> TracksCollection { get; set; }

        public Track SelectedTrack
        {
            get => selectedTrack;
            set => SetField(ref selectedTrack, value);
        }
        #endregion

        public AddTracksViewModel(IDataPathService pathService,
                                  IContentManager<Track, Playlist> contentManager)
        {
            this.pathService = pathService;
            this.contentManager = contentManager;

            CancelCommand = new RelayCommand<object>(o => Cancel());
            AddTrackCommand = new RelayCommand<object>(o => AddTrack());
            DeleteTrackCommand = new RelayCommand<object>(o => DeleteTrack(), o => SelectedTrack != null);
            ChangeImageCommand = new RelayCommand<object>(o => ChangeImage(), o => SelectedTrack != null);
            SaveCommand = new RelayCommand<object>(o => Save(), o => TracksCollection != null && TracksCollection?.Count > 0);

            TracksCollection = new();
        }

        private void Save()
        {
            CloseRequested?.Invoke(this, new DialogCreateRequestArgs(TracksCollection));
        }

        private void ChangeImage()
        {
            OpenFileDialog dlg = new()
            {
                DefaultExt = ".png",
                Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg",
                Multiselect = false
            };

            bool? result = dlg.ShowDialog();

            if (result is not true) return;
            SelectedTrack.ImageSource = dlg.FileName;
            OnPropertyChanged(nameof(SelectedTrack));
        }

        private void DeleteTrack()
        {
            TracksCollection.Remove(SelectedTrack);
        }

        private Task AddTrack()
        {
            OpenFileDialog dlg = new()
            {
                DefaultExt = ".mp3",
                Multiselect = true
            };

            bool? result = dlg.ShowDialog();

            if (result is true)
            {
                foreach (string trackPath in dlg.FileNames)
                {
                    TracksCollection.Add(CreateTrackModel(trackPath).Result);
                }
            }

            return Task.CompletedTask;
        }

        private Task<Track> CreateTrackModel(string trackPath)
        {
            TagLib.File trackFile = TagLib.File.Create(trackPath);

            Track track = new()
            {
                Id = Guid.NewGuid(),
                FileName = Path.GetFileName(trackPath),
                Artists = trackFile.Tag.JoinedPerformers ?? "Author",
                TrackAlbum = trackFile.Tag.Album ?? "Album",
                TrackSource = trackPath,
                AddedDate = DateTime.Now,
                RecentlyPlay = DateTime.Now,
                Duration = trackFile.Properties.Duration,
                IsLiked = false,
            };

            track.ImageSource = GenerateImageFromTrack(trackFile.Tag.Pictures, track.Id);
            track.Title = trackFile.Tag.Title ?? track.FileName;

            return Task.FromResult(track);
        }

        private string GenerateImageFromTrack(IPicture[] pictures, Guid trackId)
        {
            try
            {
                if (pictures is null || pictures.Length == 0)
                {
                    return pathService.DefaultTrackImagePath;
                }

                IPicture picture = pictures[0];

                string imgPath = Path.Combine(pathService.ImagesDirectoryPath, "img_" + trackId + ".png");
                using (Image img = Image.FromStream(new MemoryStream(picture.Data.Data)))
                {
                    img.Save(imgPath, ImageFormat.Png);
                }

                if (System.IO.File.Exists(imgPath))
                {
                    return imgPath;
                }
                else
                {
                    throw new Exception("Image import error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void Cancel()
        {
            CloseRequested?.Invoke(null, new DialogCreateRequestArgs(null));
        }

        public override void Dispose()
        {
            CloseRequested = null;
            GC.SuppressFinalize(TracksCollection);
            base.Dispose();
        }
    }
}

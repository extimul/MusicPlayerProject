using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.DataPath;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        #region Fields
        private Playlist playlist;
        private readonly IAudioService audioService;
        private readonly INavigatorService navigator;
        private readonly IDataPathService pathService;
        #endregion

        #region Properties
        public Playlist CurrentPlaylist
        {
            get => playlist;
            set => SetField(ref playlist, value);
        }

        public ObservableCollection<Track> TracksCollection => ControlBarViewModel.FilteredCollection;

        public Track SelectedTrack
        {
            get => audioService.SelectedTrack;
            set
            {
                audioService.SelectedTrack = value;
                OnPropertyChanged(nameof(SelectedTrack));
            }
        }

        public PlaylistControlBarViewModel ControlBarViewModel { get; set; }

        #endregion

        #region Commands
        public ICommand GoBackCommand { get; }

        #endregion

        public PlaylistViewModel(Playlist playlist, IAudioService audioService, INavigatorService navigator, IDataPathService pathService)
        {
            CurrentPlaylist = playlist;
            ControlBarViewModel = new PlaylistControlBarViewModel(this);
            ControlBarViewModel.PropertyChanged += ControlBarViewModel_PropertyChanged;

            this.audioService = audioService;
            this.navigator = navigator;
            this.pathService = pathService;
            
            this.audioService.ActivePlaylist = TracksCollection;
            this.audioService.SelectedTrack = TracksCollection[0];
            this.audioService.TrackChanged += OnTrackChanged;

            GoBackCommand = new RenavigateCommand(navigator);
        }

        private void ControlBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TracksCollection));
            OnPropertyChanged(nameof(SelectedTrack));
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
        }

        public override void Dispose()
        {
            this.audioService.TrackChanged -= OnTrackChanged;
            ControlBarViewModel.PropertyChanged -= ControlBarViewModel_PropertyChanged;
            ControlBarViewModel.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
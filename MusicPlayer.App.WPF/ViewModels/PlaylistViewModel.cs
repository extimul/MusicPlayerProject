using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Core.Enums;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.ViewModels
{
    public sealed class PlaylistViewModel : ListViewModelBase
    {
        #region Fields
        private Playlist playlist;
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        #endregion

        #region Properties
        public Playlist CurrentPlaylist
        {
            get => playlist;
            set => SetField(ref playlist, value);
        }
        public override Track SelectedTrack
        {
            get => audioService.SelectedTrack;
            set
            {
                audioService.SelectedTrack = value;
                OnPropertyChanged(nameof(SelectedTrack));
            }
        }
        public override int SelectedTrackIndex
        {
            get => audioService.SelectedTrackIndex;
            set
            {
                audioService.SelectedTrackIndex = value;
                OnPropertyChanged(nameof(SelectedTrackIndex));
            }
        }
        public override ObservableCollection<Track> TracksCollection => ControlBarViewModel.FilterPanelViewModel.FilteredCollection;
        public override ObservableCollection<MenuItemObject> ContextMenuItems { get; set; }
        public override DrawingBrush PlayPauseIcon => iconManager.PlayPauseIcon;
        #endregion

        #region Commands
        public ICommand GoBackCommand { get; set; }
        public override ICommand PlayPauseCommand { get; set; }
        public override ICommand ContextMenuCommand { get; set; }
        #endregion

        public PlaylistViewModel(Playlist playlist,
                                IAudioService audioService, 
                                IIconManager iconManager, 
                                INavigatorService navigator,
                                IContentManager<Playlist> contentManager)
        {
            this.audioService = audioService;
            this.iconManager = iconManager;

            CurrentPlaylist = playlist;
            ControlBarViewModel = new PlaylistControlBarViewModel(this);
            ControlBarViewModel.FilterPanelViewModel.PropertyChanged += FilterPanelViewModel_PropertyChanged;

            this.audioService.ActivePlaylist = TracksCollection;
            this.audioService.SelectedTrack = (TracksCollection?.Count > 0) ? TracksCollection[0] : null;
            this.audioService.TrackChanged += OnTrackChanged;
            this.audioService.IconChanged += OnIconChanged;

            GoBackCommand = new RenavigateCommand(navigator);
            PlayPauseCommand = new PlayerControlsCommand(audioService, this);
            ContextMenuCommand = new ContextMenuCommand<Playlist>(this, audioService, contentManager);

            LoadContextMenuItems();
        }

        public override void LoadContextMenuItems()
        {
            ContextMenuItems = new ObservableCollection<MenuItemObject>()
            {
                new MenuItemObject()
                {
                    Name = "Play",
                    Icon = iconManager.GetIcon(Icons.PlayIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.Play
                },
                new MenuItemObject()
                {
                    Name = "Pause",
                    Icon = iconManager.GetIcon(Icons.PauseIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.Pause
                },
                new MenuItemObject()
                {
                    Name = "Remove form playlist",
                    Icon = iconManager.GetIcon(Icons.DeleteIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.RemoveFromCollection
                },
                new MenuItemObject()
                {
                    Name = "Save to your Liked Songs",
                    Icon = iconManager.GetIcon(Icons.SaveIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.AddToLiked
                },
                new MenuItemObject()
                {
                    Name = "Get inforamtion",
                    Icon = iconManager.GetIcon(Icons.InfoIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.GetInformation
                }
            };
        }

        #region Events methods

        private void FilterPanelViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TracksCollection));
            OnPropertyChanged(nameof(SelectedTrack));
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            OnPropertyChanged(nameof(PlayPauseIcon));
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
            OnPropertyChanged(nameof(SelectedTrackIndex));
        }


        #endregion

        public override void Dispose()
        {
            audioService.IconChanged -= OnIconChanged;
            audioService.TrackChanged -= OnTrackChanged;
            ControlBarViewModel.FilterPanelViewModel.PropertyChanged -= FilterPanelViewModel_PropertyChanged;
            ControlBarViewModel.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
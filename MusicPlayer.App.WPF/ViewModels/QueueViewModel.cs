using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.ViewModels
{
    public sealed class QueueViewModel : ListViewModelBase
    {
        #region Fields
        private readonly IAudioService audioService;
        private readonly IIconManager iconManager;
        private readonly IContentManager<Track, Queue> contentManager;
        #endregion

        #region Properties
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
        public override ObservableCollection<Track> TracksCollection => contentManager.MusicModelsCollection;
        public override DrawingBrush PlayPauseIcon => iconManager.PlayPauseIcon;
        public override ObservableCollection<MenuItemObject> ContextMenuItems { get; set; }
        #endregion

        #region Commands
        public override ICommand PlayPauseCommand { get; set; }
        public override ICommand ContextMenuCommand { get; set; }
        #endregion

        public QueueViewModel(IAudioService audioService,
                              IIconManager iconManager,
                              IContentManager<Track, Queue> contentManager)
        {
            this.audioService = audioService;
            this.iconManager = iconManager;
            this.contentManager = contentManager;

            this.audioService.IconChanged += OnIconChanged;
            this.audioService.TrackChanged += OnTrackChanged;

            this.audioService.ActivePlaylist = TracksCollection;
            this.contentManager.CollectionChanged += OnQueueCollectionChanged;
            this.audioService.SelectedTrack = (TracksCollection?.Count > 0) ? TracksCollection[0] : null;

            PlayPauseCommand = new PlayerControlsCommand(audioService, this);
            ContextMenuCommand = new ContextMenuCommand<Track, Queue>(this, audioService, contentManager);
            LoadContextMenuItems();
        }

        public override void LoadContextMenuItems()
        {
            ContextMenuItems = new ObservableCollection<MenuItemObject>()
            {
                new MenuItemObject()
                {
                    Name = "Play",
                    Icon = iconManager.GetIcon(Core.Enums.Icons.PlayIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.Play
                },
                new MenuItemObject()
                {
                    Name = "Pause",
                    Icon = iconManager.GetIcon(Core.Enums.Icons.PauseIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.Pause
                },
                new MenuItemObject()
                {
                    Name = "Remove form queue",
                    Icon = iconManager.GetIcon(Core.Enums.Icons.DeleteIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.RemoveFromCollection
                },
                new MenuItemObject()
                {
                    Name = "Save to your Liked Songs",
                    Icon = iconManager.GetIcon(Core.Enums.Icons.SaveIcon),
                    MenuCommand = ContextMenuCommand,
                    CommandType = MenuCommandTypes.AddToLiked
                }
            };
        }

        private void OnQueueCollectionChanged()
        {
            OnPropertyChanged(nameof(TracksCollection));
        }

        private void OnTrackChanged()
        {
            OnPropertyChanged(nameof(SelectedTrack));
            OnPropertyChanged(nameof(SelectedTrackIndex));
        }

        private void OnIconChanged(object sender, ChangeIconEventArgs e)
        {
            if (e?.SourceState is SourceTypes.TogglePlaybackSource)
            {
                iconManager.PlayPauseIcon = iconManager?.SetPlayPauseIcon((PlaybackState)e.Value);
                OnPropertyChanged(nameof(PlayPauseIcon));
            }
        }

        public override void Dispose()
        {
            audioService.IconChanged -= OnIconChanged;
            audioService.TrackChanged -= OnTrackChanged;
            contentManager.CollectionChanged -= OnQueueCollectionChanged;
            base.Dispose();
        }
    }
}
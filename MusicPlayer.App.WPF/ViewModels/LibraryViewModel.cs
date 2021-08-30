using MusicPlayer.App.WPF.Commands;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.Services.Icon;
using MusicPlayer.App.WPF.Services.Navigators;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.App.WPF.ViewModels.Controls;
using MusicPlayer.App.WPF.ViewModels.Factories;
using MusicPlayer.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        #region Fields
        private Playlist _selectedPlaylist;
        private readonly IContentManager<Playlist> contentManager;
        #endregion

        #region Properties
        public ObservableCollection<Playlist> PlaylistCollection => FilterPanelViewModel.FilteredCollection;

        public Playlist SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                if (value == null)
                {
                    _selectedPlaylist = null;
                    return;
                }
                if (value.Equals(_selectedPlaylist)) return;
                _selectedPlaylist = value;
                OnPropertyChanged(nameof(SelectedPlaylist));
            }
        }

        public FilterHandlerPanelViewModel<Playlist> FilterPanelViewModel { get; private set; }
        #endregion

        #region Commands
        public ICommand SortCommand { get; }
        public ICommand CreatePlaylistCommand { get; }
        public ICommand OpenPlaylistCommand { get; }
        #endregion

        public LibraryViewModel(IContentManager<Playlist> contentManager,
                                INavigatorService navigator,
                                IViewModelFactory viewModelFactory,
                                IDataPathService pathService,
                                IAudioService audioService,
                                IIconManager iconManager)
        {
            this.contentManager = contentManager;
            this.contentManager.CollectionChanged += OnPlaylistCollectionChanged;

            FilterPanelViewModel = new FilterHandlerPanelViewModel<Playlist>(this.contentManager.MusicModelsCollection);
            FilterPanelViewModel.PropertyChanged += FilterPanelViewModel_PropertyChanged;

            SortCommand = new SortPlaylistsCommand();
            CreatePlaylistCommand = new CreatePlaylistCommand(this, pathService, contentManager);
            OpenPlaylistCommand = new OpenPlaylistCommand(this, audioService, iconManager, navigator, viewModelFactory, contentManager);
        }

        private void FilterPanelViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(PlaylistCollection));
        }

        private void OnPlaylistCollectionChanged()
        {
            OnPropertyChanged(nameof(PlaylistCollection));
        }

        public override void Dispose()
        {
            contentManager.CollectionChanged -= OnPlaylistCollectionChanged;
            FilterPanelViewModel.PropertyChanged -= FilterPanelViewModel_PropertyChanged;
            base.Dispose();
        }
    }
}

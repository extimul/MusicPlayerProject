using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace MusicPlayer.App.WPF.ViewModels
{
    public class PlaylistControlBarViewModel : ViewModelBase
    {
        #region Fields
        private readonly PlaylistViewModel listViewModel;
        private CollectionViewSource tracksCollection;
        private string searchingPattern;
        #endregion

        #region Properties
        //public ICollectionView FilteredCollection => tracksCollection.View;
        public ObservableCollection<Track> FilteredCollection
        {
            get
            {
                var l = tracksCollection.View.Cast<Track>().ToList();
                return new ObservableCollection<Track>(l);
            }
        }

        public string SearchingPattern
        {
            get => searchingPattern;
            set
            {
                if (value.Equals(searchingPattern)) return;
                searchingPattern = value;
                tracksCollection.View.Refresh();
                OnPropertyChanged(nameof(SearchingPattern));
            }
        }

        #endregion

        public PlaylistControlBarViewModel(PlaylistViewModel listViewModel)
        {
            this.listViewModel = listViewModel;
            tracksCollection = new CollectionViewSource()
            {
                Source = listViewModel.CurrentPlaylist.Tracks
            };
            tracksCollection.Filter += OnCollectionFiltered;
        }

        private void OnCollectionFiltered(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchingPattern))
            {
                e.Accepted = true;
                return;
            }

            Track usr = e.Item as Track;
            if (usr.TrackTitle.ToUpper().Contains(SearchingPattern.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public override void Dispose()
        {
            tracksCollection.Filter -= OnCollectionFiltered;
            base.Dispose();
        }
    }
}

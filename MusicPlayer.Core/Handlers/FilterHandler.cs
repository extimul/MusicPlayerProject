using MusicPlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MusicPlayer.Core.Handlers
{
    public class FilterHandler<T> : IDisposable where T : BaseModel
    {
        #region Events
        public event Action StateChanged;
        #endregion

        #region Fields
        private CollectionViewSource itemCollection;
        private string searchingPattern;

        #endregion

        #region Properties

        public ObservableCollection<T> FilteredCollection
        {
            get
            {
                var l = itemCollection.View.Cast<T>().ToList();
                return new ObservableCollection<T>(l);
            }
        }

        public string SearchingPattern
        {
            get => searchingPattern;
            set
            {
                if (value.Equals(searchingPattern)) return;
                searchingPattern = value;
                itemCollection.View.Refresh();
                StateChanged?.Invoke();
            }
        }

        #endregion

        public FilterHandler(IEnumerable<T> collection)
        {
            itemCollection = new CollectionViewSource()
            {
                Source = collection
            };
            itemCollection.Filter += OnCollectionFiltered;
        }

        private void OnCollectionFiltered(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchingPattern))
            {
                e.Accepted = true;
                return;
            }

            T usr = e.Item as T;
            if (usr.Title.ToUpper().Contains(SearchingPattern.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public void Dispose()
        {
            itemCollection.Filter -= OnCollectionFiltered;
        }
    }
}

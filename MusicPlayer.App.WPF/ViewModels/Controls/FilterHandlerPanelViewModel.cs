using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Handlers;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.MVVMBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicPlayer.App.WPF.ViewModels.Controls
{
    public sealed class FilterHandlerPanelViewModel<T> : ViewModelBase where T : BaseMusicPlayerModel
    {
        #region Fields
        private readonly FilterHandler<T> filterHandler;
        #endregion

        #region Properties
        public ObservableCollection<T> FilteredCollection
        {
            get => filterHandler.FilteredCollection;
        }

        public string SearchingPattern
        {
            get => filterHandler.SearchingPattern;
            set
            {
                if (value.Equals(filterHandler.SearchingPattern)) return;
                filterHandler.SearchingPattern = value;
                OnPropertyChanged(nameof(SearchingPattern));
            }
        }

        #endregion

        public FilterHandlerPanelViewModel(IEnumerable<T> itemsCollection)
        {
            filterHandler = new FilterHandler<T>(itemsCollection);
            filterHandler.StateChanged += FilterHandler_StateChanged;
        }

        private void FilterHandler_StateChanged()
        {
            OnPropertyChanged(nameof(SearchingPattern));
            OnPropertyChanged(nameof(FilteredCollection));
        }

        public override void Dispose()
        {
            filterHandler.StateChanged -= FilterHandler_StateChanged;
            filterHandler.Dispose();
            base.Dispose();
        }
    }
}
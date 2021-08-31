using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Services.Content
{
    public interface IContentManager<T, U>
    {
        event Action CollectionChanged;
        ObservableCollection<T> MusicModelsCollection { get; }
        Task Add(T item);
        Task Delete(T item);
        Task Update(T item);
        Task LoadData(object data = null);
    }
}
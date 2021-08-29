using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IContentManager<T>
    {
        event Action CollectionChanged;
        ObservableCollection<T> MusicModelsCollection { get; }
        Task Add(T item);
        Task Delete(int id);
        Task GetModel();
        Task Update(T item);
    }
}
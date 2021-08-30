using MusicPlayer.Core.Models;
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
        Task Delete(T item);
        Task Update(T item);
    }
}
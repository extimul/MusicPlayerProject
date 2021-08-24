using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Audio
{
    public interface ITracksCollectionService<T> where T : BaseModel
    {
        public event Action CollectionChanged;
        public ObservableCollection<T> TracksCollection { get; set; }
        Task LoadCollection { get; }
        Task AddItem(T item);
        Task DeleteItem(int id);
        Task UpdateItem(T playlist);
    }
}

using MusicPlayer.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IContentLoader<T> where T : BaseMusicPlayerModel
    {
        Task<ObservableCollection<T>> LoadCollection(string path);
        Task UpdateJsonFile(string path, ObservableCollection<T> newCollection);
    }
}
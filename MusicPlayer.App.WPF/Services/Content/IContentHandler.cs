using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IContentHandler<T> where T : BaseModel
    {
        Task<ObservableCollection<T>> LoadCollection(string path);
        Task UpdateJsonFile(string path, ObservableCollection<T> newCollection);
    }
}
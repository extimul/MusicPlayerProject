using MusicPlayer.Core.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public class ContentHandler<T> : IContentHandler<T> where T : BaseModel
    {
        public Task<ObservableCollection<T>> LoadCollection(string path)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<T>>(File.ReadAllText(path)));
        }

        public Task UpdateJsonFile(string path, ObservableCollection<T> newCollection)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new();
                serializer.Serialize(file, newCollection);
            }
            return Task.CompletedTask;
        }
    }
}

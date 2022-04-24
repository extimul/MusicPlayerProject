using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Handlers
{
    public static class FileManager<T>
    {
        public static Task UpdateFile(T model, string path)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new();
                serializer.Serialize(file, model);
            }
            return Task.CompletedTask;
        }

        public static Task<T> LoadModelFromFile(string path)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<T>(File.ReadAllText(path)));
        }
    }
}

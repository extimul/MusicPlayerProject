using MusicPlayer.Core.Handlers;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Services.Content
{
    public sealed class ContentContainer<T> : IContentContainer<T>
    {
        public T Model { get; set; }
        
        public async Task LoadContent(string filePath)
        {
            Model = await FileManager<T>.LoadModelFromFile(filePath);
        }

        public async Task<T> LoadModel(string filePath)
        {
            return await FileManager<T>.LoadModelFromFile(filePath);
        }

        public async Task UpdateContent(string filePath)
        {
            await FileManager<T>.UpdateFile(Model, filePath);
        }

        public async Task UpdateContent(string filePath, T content)
        {
            await FileManager<T>.UpdateFile(content, filePath);
        }
    }
}

using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public interface IContentContainer<T>
    {
        T Model { get; set; }
        Task LoadContent(string filePath);
        Task UpdateContent(string filePath);
        Task UpdateContent(string filePath, T content);
        Task<T> LoadModel(string filePath);
    }
}
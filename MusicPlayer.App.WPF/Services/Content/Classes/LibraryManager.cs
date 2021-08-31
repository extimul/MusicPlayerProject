using MusicPlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public class LibraryManager : IContentManager<Playlist, Library>
    {
        private readonly IContentContainer<Playlist> contentContainer;
        private readonly IDataPathService pathService;
        #region Event
        public event Action CollectionChanged;
        #endregion

        #region Properties
        public ObservableCollection<Playlist> MusicModelsCollection { get; set; }
        public Task LoadTask { get; }
        #endregion

        public LibraryManager(IContentContainer<Playlist> contentContainer, IDataPathService pathService)
        {
            this.contentContainer = contentContainer;
            this.pathService = pathService;
            LoadTask = LoadData();
        }

        public async Task LoadData(object data = null)
        {
            List<string> playlists = pathService.GetPlaylistFileNames("playlist");

            if (MusicModelsCollection is null)
            {
                MusicModelsCollection = new ObservableCollection<Playlist>();
            }

            foreach (string playlist in playlists)
            {
                MusicModelsCollection.Add(await contentContainer.LoadModel(playlist));
            }

            CollectionChanged?.Invoke();
        }

        public async Task Add(Playlist playlist)
        {
            if (playlist != null)
            {
                contentContainer.Model = playlist;
                MusicModelsCollection.Add(playlist);
                await contentContainer.UpdateContent(pathService.GeneratePlaylistJsonFileName(playlist.Id.ToString()));

                CollectionChanged?.Invoke();
            }
        }

        public Task Update(Playlist item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Playlist item)
        {
            throw new NotImplementedException();
        }
    }
}

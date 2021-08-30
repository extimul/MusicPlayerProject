using MusicPlayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public class PlaylistManager : IContentManager<Playlist>
    {
        private readonly IContentContainer<Playlist> contentContainer;
        private readonly IDataPathService pathService;
        #region Event
        public event Action CollectionChanged;
        #endregion

        #region Properties
        public ObservableCollection<Playlist> MusicModelsCollection { get; set; }
        public Task Load { get; }
        #endregion

        public PlaylistManager(IContentContainer<Playlist> contentContainer, IDataPathService pathService)
        {
            this.contentContainer = contentContainer;
            this.pathService = pathService;
            Load = LoadPlaylists();
        }

        private async Task LoadPlaylists()
        {
            List<string> playlists = pathService.GetFileNames("playlist");

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
                await contentContainer.UpdateContent(pathService.GeneratJsonFileName(playlist.Id.ToString()));

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

using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Services.Content
{
    public class TracksManager : IContentManager<Track, Playlist>
    {
        #region Events
        public event Action CollectionChanged;
        #endregion

        #region Fields
        private readonly IContentContainer<Playlist> contentContainer;
        private readonly IDataPathService dataPath;
        #endregion

        #region Properties
        public ObservableCollection<Track> MusicModelsCollection { get; set; }
        
        #endregion
 
        public TracksManager(IContentContainer<Playlist> contentContainer, IDataPathService dataPath)
        {
            this.contentContainer = contentContainer;
            this.dataPath = dataPath;
        }

        public async Task Add(Track track)
        {
            if (track != null)
            {
                contentContainer.Model.TracksCollection.Add(track);

                await contentContainer.UpdateContent(dataPath.GeneratePlaylistJsonFileName(contentContainer.Model.Id.ToString()));

                CollectionChanged?.Invoke();
            }
        }

        public Task Delete(Track item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Track item)
        {
            throw new NotImplementedException();
        }

        public Task LoadData(object data)
        {
            if (typeof(Playlist) == data.GetType())
            {
                Playlist currentPlaylist = (Playlist)data;
                contentContainer.Model = currentPlaylist;
                MusicModelsCollection = currentPlaylist.TracksCollection;

                CollectionChanged?.Invoke();
            }
            return Task.CompletedTask;
        }
    }
}
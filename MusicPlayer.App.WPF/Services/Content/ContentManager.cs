using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Content
{
    public sealed class ContentManager<T> : IContentManager<T> where T : BaseMusicPlayerModel
    {
        #region Events
        public event Action CollectionChanged;
        #endregion

        #region Fields

        private readonly IContentLoader<T> contentLoader;
        private readonly IDataPathService pathManager;
        private string path = "";
        #endregion

        #region Properties
        public ObservableCollection<T> MusicModelsCollection { get; set; }

        public Task GetModelTask { get; }

        #endregion
        public ContentManager(IContentLoader<T> contentLoader, IDataPathService pathManager)
        {
            this.contentLoader = contentLoader;
            this.pathManager = pathManager;
            SetPath();
            GetModelTask = GetModel();
        }

        private void SetPath()
        {
            Type type = typeof(T);
            if (type == typeof(Playlist))
            {
                path = pathManager.PlaylistJsonPath;
            }
            else if (type == typeof(Track))
            {
                path = pathManager.QueueJsonPath;
            }
        }

        public async Task Add(T item)
        {
            if (MusicModelsCollection != null)
            {
                if (MusicModelsCollection.Count > 0)
                {
                    item.Id = MusicModelsCollection[^1].Id + 1;
                }
                else
                {
                    item.Id += 1;
                }
                MusicModelsCollection.Add(item);

                await contentLoader.UpdateJsonFile(path, MusicModelsCollection);
                CollectionChanged?.Invoke();
            }
        }

        public async Task Delete(int id)
        {

            //if (TracksCollection is null) return;
            //TracksCollection.Remove(TracksCollection.Single(item => item.Id == id));

            //await contentHandler.UpdateJsonFile(filePath, TracksCollection);
            //CollectionChanged?.Invoke();
        }

        public async Task Update(T item)
        {
            //T item = TracksCollection.AsParallel().Where(i => i.Id == newItem.Id).FirstOrDefault();

            //if (item is not null)
            //{
            //    TracksCollection[item.GetId()] = newItem;
            //}

            //await contentHandler.UpdateJsonFile(filePath, TracksCollection);
            //CollectionChanged?.Invoke();
        }

        public async Task GetModel()
        {
            MusicModelsCollection = await contentLoader.LoadCollection(path);
            if (MusicModelsCollection is null)
            {
                await CreateCollectionModel();
            }
        }

        private async Task CreateCollectionModel()
        {
            MusicModelsCollection = Activator.CreateInstance<ObservableCollection<T>>();
            await contentLoader.UpdateJsonFile(path, MusicModelsCollection);
        }
    }
}

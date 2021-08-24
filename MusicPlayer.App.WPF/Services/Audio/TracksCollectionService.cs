using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Audio
{
    public class TracksCollectionService<T> : ITracksCollectionService<T> where T : BaseModel
    {
        #region Events
        public event Action CollectionChanged;
        #endregion

        #region Fields
        private readonly IDataPathService dataPathService;
        private readonly IContentHandler<T> contentHandler;
        private string filePath;

        private ObservableCollection<T> tracksCollection;
        #endregion

        #region Properties
        public ObservableCollection<T> TracksCollection
        {
            get => tracksCollection;
            set
            {
                if (value.Equals(tracksCollection)) return;
                tracksCollection = value;
                CollectionChanged?.Invoke();
            }
        }
        public Task LoadCollection { get; }
        #endregion

        #region Constructor
        public TracksCollectionService(IDataPathService dataPathService, IContentHandler<T> contentLoader)
        {
            this.dataPathService = dataPathService;
            this.contentHandler = contentLoader;

            SetPath();
            this.LoadCollection = InitLoadCollectionTask();
        }
        #endregion

        #region Methods

        #region InitMethods
        private async Task InitLoadCollectionTask()
        {
            TracksCollection = await contentHandler.LoadCollection(filePath);
            CollectionChanged?.Invoke();
        }

        private void SetPath()
        {
            Type type = typeof(T);
            if (type == typeof(Playlist))
                filePath = dataPathService.PlaylistJsonPath;
            else if (type == typeof(Track))
                filePath = dataPathService.QueueJsonPath;
        }

        #endregion

        #region Content Proccesing

        public async Task AddItem(T item)
        {
            if (TracksCollection is null) return;
            item.Id = TracksCollection[^1].Id + 1;
            TracksCollection.Add(item);

            await contentHandler.UpdateJsonFile(filePath, TracksCollection);
            CollectionChanged?.Invoke();
        }

        public async Task DeleteItem(int id)
        {
            if (TracksCollection is null) return;
            TracksCollection.Remove(TracksCollection.Single(item => item.Id == id));

            await contentHandler.UpdateJsonFile(filePath, TracksCollection);
            CollectionChanged?.Invoke();
        }

        public async Task UpdateItem(T newItem)
        {
            T item = TracksCollection.AsParallel().Where(i => i.Id == newItem.Id).FirstOrDefault();

            if (item is not null)
            {
                TracksCollection[item.GetId()] = newItem;
            }

            await contentHandler.UpdateJsonFile(filePath, TracksCollection);
            CollectionChanged?.Invoke();
        }

        #endregion
        #endregion
    }
}

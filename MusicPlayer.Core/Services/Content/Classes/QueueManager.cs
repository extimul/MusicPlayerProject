using MusicPlayer.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Services.Content
{
    public sealed class QueueManager : IContentManager<Track, Queue>
    {
        public event Action CollectionChanged;

        private readonly IContentContainer<Queue> contentContainer;
        private readonly IDataPathService pathService;

        public ObservableCollection<Track> MusicModelsCollection => contentContainer.Model?.TracksCollection;

        public QueueManager(IContentContainer<Queue> contentContainer, IDataPathService pathService)
        {
            this.contentContainer = contentContainer;
            this.pathService = pathService;

            LoadData(); // I don't know how to upload data correctly :(
        }

        //private void Ser()
        //{
        //    Queue q = new()
        //    {
        //        TracksCollection =
        //        {
        //            new Track()
        //            {
        //                Id = Guid.NewGuid(),
        //                Author = "grandson",
        //                TrackAlbum = "Rain",
        //                Title = "Rain",
        //                ImageSource = "D:\\Projects\\C#\\MusicPlayerProject\\MusicPlayer.Core\\bin\\Debug\\net5.0-windows\\ApplicationResources\\DefaultSongImg.png",
        //                TrackSource = "D:\\Projects\\C#\\MusicPlayerProject\\MusicPlayer.Core\\bin\\Debug\\net5.0-windows\\Musics\\track1.mp3",
        //                IsLiked = true,
        //                AddedDate = DateTime.Now,
        //                RecentlyPlay = DateTime.Now,
        //                Duration = TimeSpan.FromSeconds(300),
        //            }
        //        }
        //    };

        //    contentContainer.Model = q;
        //    contentContainer.UpdateContent(pathService.QueueJsonPath);
        //}

        public async Task Add(Track track)
        {
            if (track != null)
            {
                ObservableCollection<Track> collection = contentContainer.Model?.TracksCollection;

                if (collection != null)
                {
                    collection.Add(track);
                    contentContainer.Model.TracksCollection = collection;
                }
                await contentContainer.UpdateContent(pathService.QueueJsonPath);
                CollectionChanged?.Invoke();
            }
        }

        public async Task Delete(Track track)
        {
            if (track != null)
            {
                contentContainer.Model?.TracksCollection.Remove(track);
                await contentContainer.UpdateContent(pathService.QueueJsonPath);
                CollectionChanged?.Invoke();
            }
        }

        public Task Update(Track item)
        {
            throw new NotImplementedException();
        }

        public Task LoadData(object data = null)
        {
            this.contentContainer.LoadContent(pathService.QueueJsonPath);
            return Task.CompletedTask;
        }
    }
}
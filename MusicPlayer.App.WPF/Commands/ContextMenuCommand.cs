using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class ContextMenuCommand<T, U> : AsyncCommandBase where T : BaseMusicPlayerModel
    {
        private readonly ListViewModelBase viewModel;
        private readonly IAudioService audioService;
        private readonly IContentManager<T, U> contentManger;
        //private readonly IContentManager<Track> queueContentManager;

        public ContextMenuCommand(ListViewModelBase viewModel, IAudioService audioService, IContentManager<T, U> contentManager)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.contentManger = contentManager;
        }

        //public ContextMenuCommand(ListViewModelBase viewModel,
        //                          IAudioService audioService,
        //                          IContentManager<T, U> contentManager,
        //                          IContentManager<Track> queueContentManager) : this(viewModel, audioService, contentManager)
        //{
        //    this.queueContentManager = queueContentManager;
        //}

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is MenuCommandTypes commandType)
            {
                switch (commandType)
                {
                    case MenuCommandTypes.Play:
                        await audioService.PlayTrack();
                        break;
                    case MenuCommandTypes.Pause:
                        await audioService.StopTrack();
                        break;
                    case MenuCommandTypes.RemoveFromCollection:
                        //await contentManger.DeleteItem(viewModel.SelectedTrack.Id);
                        break;
                    case MenuCommandTypes.AddToQueue:
                        //await queueContentManager.Add(viewModel.SelectedTrack);
                        break;
                    case MenuCommandTypes.AddToLiked:
                        break;
                    case MenuCommandTypes.GetInformation:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

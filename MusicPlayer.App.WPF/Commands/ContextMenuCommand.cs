﻿using MusicPlayer.App.WPF.Commands.Base;
using MusicPlayer.App.WPF.Services.Audio;
using MusicPlayer.App.WPF.Services.Content;
using MusicPlayer.App.WPF.ViewModels.Base;
using MusicPlayer.Core.Models;
using MusicPlayer.Core.Types;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Commands
{
    public class ContextMenuCommand<T> : AsyncCommandBase where T : BaseMusicPlayerModel
    {
        private readonly ListViewModelBase viewModel;
        private readonly IAudioService audioService;
        private readonly IContentManager<T> contentManger;

        public ContextMenuCommand(ListViewModelBase viewModel, IAudioService audioService, IContentManager<T> contentManager)
        {
            this.viewModel = viewModel;
            this.audioService = audioService;
            this.contentManger = contentManager;
        }

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

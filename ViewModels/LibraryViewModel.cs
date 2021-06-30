using MusicPlayerProject.Core.Commands;
using MusicPlayerProject.Core.Managers.Dialog;
using MusicPlayerProject.Core.Models;
using MusicPlayerProject.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
{
    public class LibraryViewModel : ViewModelBase
    {
        private ObservableCollection<Playlist> _collection;
        public ObservableCollection<Playlist> Collection
        {
            get => _collection;
            set
            {
                if (value.Equals(_collection)) return;
                _collection = value;
                OnPropertyChanged(nameof(Collection));
            }
        }

        public ICommand SortCommand { get; }
        public ICommand CreatePlaylistCommand { get; }

        public LibraryViewModel(IDialogService dialogService)
        {
            SortCommand = new SortPlaylistsCommand();
            CreatePlaylistCommand = new CreatePlaylistCommand(this);
            Collection = new ObservableCollection<Playlist>()
            {
                new Playlist()
                {
                    Id = 1,
                    PlaylistName = "Liked songs",
                    Description = "Something text for this playlist and text text text text text",
                    RecentlyPlay =  DateTime.Now,
                    AddedDate = DateTime.Now,
                    Author = "You",
                    ImageSource = @"E:\Projects\VisualStudioProjects\MusicPlayerProject\ApplicationResources\DefaultSongImg.png"
                }
            };
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}

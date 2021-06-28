using MusicPlayerProject.Core.Commands;
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
        public LibraryViewModel()
        {
            SortCommand = new SortPlaylistsCommand();
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
    }
}

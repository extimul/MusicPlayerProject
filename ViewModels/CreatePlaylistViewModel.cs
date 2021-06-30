using MusicPlayerProject.Core.Commands.Base;
using MusicPlayerProject.ViewModels.Base;
using MusicPlayerProject.Core.Managers.Dialog;
using MusicPlayerProject.Core.Models;
using System;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
{
    public class CreatePlaylistViewModel : ViewModelBase, ICreatorDialog
    {
        #region Events
        public event EventHandler<DialogCreateRequestArgs> CloseRequested;
        #endregion

        #region Fields
        private string _text;

        #endregion

        #region Properties
        public string Text
        {
            get => _text;
            set
            {
                if (value.Equals(_text)) return;
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        #endregion

        #region Commands 
        public ICommand CreateCommand { get; }
        public ICommand CancelCommand { get; }
        #endregion

        public CreatePlaylistViewModel()
        {
            CreateCommand = new RelayCommand(() => CreatePlaylist());
            CancelCommand = new RelayCommand(() => Cancel());
        }

        public void CreatePlaylist()
        {
            CloseRequested?.Invoke(this, new DialogCreateRequestArgs(Text));
        }

        private void Cancel()
        {
            CloseRequested?.Invoke(this, new DialogCreateRequestArgs(null));
        }

        public override void Dispose()
        {
            CloseRequested = null;
            base.Dispose();
        }
    }
}

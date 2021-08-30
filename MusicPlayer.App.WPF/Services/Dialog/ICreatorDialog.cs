using System;
using System.Windows.Input;

namespace MusicPlayer.App.WPF.Services.Dialog
{
    public sealed class DialogCreateRequestArgs
    {
        public object Result { get; }
        public DialogCreateRequestArgs(object args)
        {
            Result = args;
        }
    }

    public interface ICreatorDialog
    {
        event EventHandler<DialogCreateRequestArgs> CloseRequested;
        ICommand CancelCommand { get; }
    }
}

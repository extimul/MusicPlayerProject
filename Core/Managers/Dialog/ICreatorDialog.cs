using System;
using System.Windows.Input;

namespace MusicPlayerProject.Core.Managers.Dialog
{
    public class DialogCreateRequestArgs
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
        ICommand CreateCommand { get; }
    }
}

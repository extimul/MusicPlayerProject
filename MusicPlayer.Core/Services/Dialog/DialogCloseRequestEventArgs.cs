namespace MusicPlayer.Core.Services.Dialog
{
    public sealed class DialogCloseRequestEventArgs
    {
        public bool? ResultValue { get; }
        public DialogCloseRequestEventArgs(bool? dialogResult)
        {
            ResultValue = dialogResult;
        }
    }
}

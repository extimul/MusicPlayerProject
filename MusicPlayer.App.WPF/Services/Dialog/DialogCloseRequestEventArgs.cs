namespace MusicPlayer.App.WPF.Services.Dialog
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

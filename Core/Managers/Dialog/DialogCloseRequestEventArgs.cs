namespace MusicPlayerProject.Core.Managers.Dialog
{
    public class DialogCloseRequestEventArgs
    {
        public bool? ResultValue { get; }

        public DialogCloseRequestEventArgs(bool? dialogResult)
        {
            ResultValue = dialogResult;
        }
    }
}

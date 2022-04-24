using MusicPlayer.Core.Types;
using System;

namespace MusicPlayer.Core.Models
{
    public sealed class ChangeIconEventArgs : EventArgs
    {
        public SourceTypes SourceState { get; set; }
        public object Value { get; set; }

        public ChangeIconEventArgs(SourceTypes sourceState, object value)
        {
            SourceState = sourceState;
            Value = value;
        }
    }
}

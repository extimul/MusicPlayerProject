using MusicPlayerProject.Core.Enums;
using System;

namespace MusicPlayerProject.Core.Models
{
    public class ChangeIconEventArgs : EventArgs
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

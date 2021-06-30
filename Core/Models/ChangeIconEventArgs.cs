using MusicPlayerProject.Core.Enums;
using System;

namespace MusicPlayerProject.Core.Models
{
    public class ChangeIconEventArgs : EventArgs
    {
        public Icons Icon { get; set; }
        public SourceTypes SourceState { get; set; }
        public object Value { get; set; }

        public ChangeIconEventArgs(SourceTypes sourceState, object value)
        {
            SourceState = sourceState;
            Value = value;
        }
    }
}

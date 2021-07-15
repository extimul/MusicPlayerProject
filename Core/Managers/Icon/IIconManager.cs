using MusicPlayerProject.Core.Enums;
using NAudio.Wave;
using System.Windows.Media;

namespace MusicPlayerProject.Core.Managers.Icon
{
    public interface IIconManager
    {
        public DrawingBrush PlayPauseIcon { get; set; }
        public DrawingBrush VolumeIcon { get; set; }
        DrawingBrush GetIcon(Icons icon);
        DrawingBrush SetPlayPauseIcon(PlaybackState state);
        DrawingBrush SetVolumeIcon(double volumeValue);
    }
}

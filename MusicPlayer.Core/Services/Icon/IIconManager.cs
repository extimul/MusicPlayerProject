using MusicPlayer.Core.Types;
using NAudio.Wave;
using System.Windows.Media;

namespace MusicPlayer.Core.Services.Icon
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

using MusicPlayer.Core.Types;
using NAudio.Wave;
using System.Windows;
using System.Windows.Media;

namespace MusicPlayer.Core.Services.Icon
{
    public sealed class IconManager : IIconManager
    {
        #region Field
        private DrawingBrush _playPauseIcon;
        private DrawingBrush _volumeIcon;
        #endregion

        public DrawingBrush PlayPauseIcon
        {
            get => _playPauseIcon;
            set
            {
                if (value.Equals(_playPauseIcon)) return;
                _playPauseIcon = value;
            }
        }

        public DrawingBrush VolumeIcon
        {
            get => _volumeIcon;
            set
            {
                if (value.Equals(_volumeIcon)) return;
                _volumeIcon = value;
            }
        }

        public DrawingBrush GetIcon(Icons icon)
        {
            return (DrawingBrush)Application.Current.FindResource(icon.ToString());
        }

        public DrawingBrush SetPlayPauseIcon(PlaybackState state)
        {
            return state switch
            {
                PlaybackState.Stopped => GetIcon(Icons.PlayIcon),
                PlaybackState.Playing => GetIcon(Icons.PauseIcon),
                PlaybackState.Paused => GetIcon(Icons.PlayIcon),
                _ => null,
            };
        }

        public DrawingBrush SetVolumeIcon(double volumeValue)
        {
            return volumeValue switch
            {
                0 => GetIcon(Icons.VolumeOffIcon),
                > 0 and <= 30.0 => GetIcon(Icons.VolumeLowIcon),
                > 30.0 and <= 65.0 => GetIcon(Icons.VolumeMediumIcon),
                > 65.0 and <= 100.0 => GetIcon(Icons.VolumeHighIcon),
                _ => null,
            };
        }
    }
}

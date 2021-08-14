using MusicPlayer.App.WPF.Core.Enums;
using NAudio.Wave;
using System.Windows;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.Services.Icon
{
    public class IconManager : IIconManager
    {
        private DrawingBrush _playPauseIcon;

        private DrawingBrush _volumeIcon;
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
            switch (state)
            {
                case PlaybackState.Stopped:
                    return GetIcon(Icons.PlayIcon);
                case PlaybackState.Playing:
                    return GetIcon(Icons.PauseIcon);
                case PlaybackState.Paused:
                    return GetIcon(Icons.PlayIcon);
                default:
                    return null;
            }
        }

        public DrawingBrush SetVolumeIcon(double volumeValue)
        {
            switch (volumeValue)
            {
                case 0:
                    return GetIcon(Icons.VolumeOffIcon);
                case > 0 and <= 30.0:
                    return GetIcon(Icons.VolumeLowIcon);
                case > 30.0 and <= 65.0:
                    return GetIcon(Icons.VolumeMediumIcon);
                case > 65.0 and <= 100.0:
                    return GetIcon(Icons.VolumeHighIcon);
                default:
                    return null;
            }
        }
    }
}

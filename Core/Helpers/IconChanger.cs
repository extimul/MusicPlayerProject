using MusicPlayerProject.Core.Enums;
using NAudio.Wave;
using System.Windows;
using System.Windows.Media;

namespace MusicPlayerProject.Core.Helpers
{
    public static class IconChanger
    {
        public static DrawingBrush SetPlayPauseIcon(object value)
        {
            var state = (PlaybackState)value;
            switch (state)
            {
                case PlaybackState.Stopped:
                    return (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];
                case PlaybackState.Playing:
                    return (DrawingBrush)Application.Current.Resources[Icons.PauseIcon.ToString()];
                case PlaybackState.Paused:
                    return (DrawingBrush)Application.Current.Resources[Icons.PlayIcon.ToString()];
                default:
                    return null;
            }
        }

        public static DrawingBrush SetVolumeIcon(object value)
        {
            double volumeValue = (double)value;

            switch (volumeValue)
            {
                case 0:
                    return (DrawingBrush)Application.Current.Resources[Icons.VolumeOffIcon.ToString()];
                case > 0 and <= 30.0:
                    return (DrawingBrush)Application.Current.Resources[Icons.VolumeLowIcon.ToString()];
                case > 30.0 and <= 65.0:
                    return (DrawingBrush)Application.Current.Resources[Icons.VolumeMediumIcon.ToString()];
                case > 65.0 and <= 100.0:
                    return (DrawingBrush)Application.Current.Resources[Icons.VolumeHighIcon.ToString()];
                default:
                    return null;
            }
        }
    }
}

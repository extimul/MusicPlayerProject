using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MusicPlayer.Core.Helpers.Converters
{
    public sealed class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PathGeometry path = new();

            if ((value as DrawingBrush)?.Drawing is DrawingGroup draw)
                foreach (var drawing in draw.Children)
                {
                    var item = (GeometryDrawing) drawing;
                    path.AddGeometry(item.Geometry);
                }

            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

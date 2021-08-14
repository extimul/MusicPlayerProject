using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MusicPlayer.App.WPF.Core.Helpers.Converters
{
    public class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DrawingGroup draw = (value as DrawingBrush)?.Drawing as DrawingGroup;
            PathGeometry path = new PathGeometry();

            if (draw != null)
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

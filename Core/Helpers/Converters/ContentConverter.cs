using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MusicPlayerProject.Core.Helpers.Converters
{
    public class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DrawingGroup draw = (value as DrawingBrush).Drawing as DrawingGroup;
            PathGeometry path = new PathGeometry();

            foreach (GeometryDrawing item in draw.Children)
            {
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

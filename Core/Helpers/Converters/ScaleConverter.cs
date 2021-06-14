using System;
using System.Globalization;
using System.Windows.Data;

namespace MusicPlayerProject.Core.Helpers.Converters
{
    public class ScaleConverter : IValueConverter
    {
        public double Scale { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = (double)value;
            return (num * (Scale / 100));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

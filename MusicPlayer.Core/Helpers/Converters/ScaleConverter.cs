using System;
using System.Globalization;
using System.Windows.Data;

namespace MusicPlayer.Core.Helpers.Converters
{
    public sealed class ScaleConverter : IValueConverter
    {
        public double Scale { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                double num = (double)value;
                return (num * (Scale / 100));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

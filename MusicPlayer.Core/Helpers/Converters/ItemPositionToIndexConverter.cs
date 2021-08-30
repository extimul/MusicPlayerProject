using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MusicPlayer.Core.Helpers.Converters
{
    public sealed class ItemPositionToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = 0;
            ListViewItem item = value as ListViewItem;

            if (item != null)
            {
                ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
                index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            }

            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

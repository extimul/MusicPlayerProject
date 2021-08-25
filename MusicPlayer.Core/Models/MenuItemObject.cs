using MusicPlayer.Core.Types;
using System.Windows.Input;
using System.Windows.Media;

namespace MusicPlayer.Core.Models
{
    public class MenuItemObject
    {
        public string Name { get; set; }
        public DrawingBrush Icon { get; set; }
        public ICommand MenuCommand { get; set; }
        public MenuCommandTypes CommandType { get; set; }
    }
}

using System.Windows;

namespace MusicPlayer.App.WPF.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}

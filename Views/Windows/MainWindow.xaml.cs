using System.Windows;

namespace MusicPlayerProject.Views.Windows
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

using MusicPlayerProject.ViewModels;
using System.Windows.Controls;

namespace MusicPlayerProject.Views.Pages
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    }
}

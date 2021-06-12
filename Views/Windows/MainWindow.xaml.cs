using MusicPlayerProject.ViewModels;
using MusicPlayerProject.Views.Pages;
using System;
using System.Windows;

namespace MusicPlayerProject.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }
    }
}

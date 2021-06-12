using MusicPlayerProject.Core.Helpers;
using MusicPlayerProject.Core.Interfaces;
using MusicPlayerProject.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicPlayerProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IWindowApp
    {
        #region private fields
        private static Window _window;

        private int _outerMarginSize = 0;

        private int _windowRadius = 10;

        private ViewModelBase _selectedViewModel = new HomeViewModel();
        #endregion

        #region Properties
        
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            { 
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public double WindowMinimumHeight { get; set; } = 720;
        public double WindowMinimumWidth { get; set; } = 1280;

        public int ResizeBorder { get { return Bordless ? 0 : 6; } }

        public int WindowRadius { get => Bordless ? 0 : _windowRadius; set => _windowRadius = value; }
        public int OuterMarginSize { get => Bordless ? 0 : _outerMarginSize; set => _outerMarginSize = value; }
        public int TitleHeight { get; set; } = 42;

        public bool Bordless { get { return (_window.WindowState == WindowState.Maximized || DockPosition != WindowDockPosition.Undocked); } }

        public Thickness InnerContentPadding { get { return new Thickness(0); } }

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(_outerMarginSize); } }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public GridLength TitleHeightGridLenght { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public ICommand MaximizeCommand { get; set; } = new RelayCommand(o => { _window.WindowState ^= WindowState.Maximized; });
        public ICommand MinimizeCommand { get; set; } = new RelayCommand(o => { _window.WindowState = WindowState.Minimized; });
        public ICommand CloseCommand { get; set; } = new RelayCommand(o => { _window.Close(); });

        public WindowDockPosition DockPosition { get; set; } = WindowDockPosition.Undocked;

        #endregion

        public MainWindowViewModel(Window window)
        {
            _window = window;

            WindowResized();

            var resizer = new WindowResizer(window);

            resizer.WindowDockChanged += (dock) =>
            {
                DockPosition = dock;

                WindowResized();
            };
        }

        public void WindowResized()
        {
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
    }
}

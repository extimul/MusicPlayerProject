using MusicPlayerProject.Core.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicPlayerProject.Core.Interfaces
{
    public interface IWindowApp
    {
        #region WindowProperties
        public WindowDockPosition DockPosition { get; set; }
        public double WindowMinimumHeight { get ; set; }
        public double WindowMinimumWidth { get; set; }
        public int ResizeBorder { get; }
        public int WindowRadius { get; set; }
        public int OuterMarginSize { get; set; }
        public int TitleHeight { get; set; }
        public bool Bordless { get; }
        public Thickness InnerContentPadding { get; }
        public Thickness ResizeBorderThickness { get; }
        public Thickness OuterMarginSizeThickness { get; }
        public CornerRadius WindowCornerRadius { get; }
        public GridLength TitleHeightGridLenght { get; }
        #endregion
        public ICommand MaximizeCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        void WindowResized();
    }
}

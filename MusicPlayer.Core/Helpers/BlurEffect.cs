using System.Windows;

namespace MusicPlayer.Core.Helpers
{
    public static class BlurEffect
    {
        public static void Apply(Window window, double effectValue = 4)
        {
            System.Windows.Media.Effects.BlurEffect blur = new System.Windows.Media.Effects.BlurEffect();
            blur.Radius = effectValue;
            window.Effect = blur;
        }
        public static void Clear(Window window)
        {
            window.Effect = null;
        }
    }
}

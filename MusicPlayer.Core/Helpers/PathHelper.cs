using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Core.Helpers
{
    public static class PathHelper
    {
        public static string GetDefaultImagePath()
        {
            string applicationPath = Directory.GetCurrentDirectory();
            string imagePath = Path.Combine(applicationPath, "ApplicationResources\\DefaultSongImg.png");

            if (File.Exists(imagePath)) return imagePath;
            else return string.Empty;
        }
    }
}

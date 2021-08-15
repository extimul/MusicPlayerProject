using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.App.WPF.Services.Settings
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        public double MusicVolumeValue { get; set; } = 50;

        public Task Save()
        {

            Trace.WriteLine("Saved");
            return Task.CompletedTask;
        }
    }
}

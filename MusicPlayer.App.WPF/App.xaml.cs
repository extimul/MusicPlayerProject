using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicPlayer.App.WPF.HostBuilders;
using MusicPlayer.App.WPF.ViewModels;
using MusicPlayer.App.WPF.Views.Windows;
using System.Windows;

namespace MusicPlayer.App.WPF
{
    public partial class App : Application
    {
        public static Window AppWindow { get; set; }

        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddServices()
                .AddViewModels()
                .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            AppWindow = _host.Services.GetRequiredService<MainWindow>();
           
            AppWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

    }
}

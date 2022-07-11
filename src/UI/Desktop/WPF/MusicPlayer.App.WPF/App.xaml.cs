using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using EasyServiceConfigurator;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.App.WPF.Services.NavigationService;
using MusicPlayer.App.WPF.Views.Windows;

namespace MusicPlayer.App.WPF;

public partial class App : Application
{
    public App()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.ConfigureServicesFromAssembly();
        Ioc.Default.ConfigureServices(serviceCollection.BuildServiceProvider());
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            SetupNavigation();
        
            Ioc.Default.GetRequiredService<MainView>().Show();
            base.OnStartup(e);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            Current.Shutdown();
        }
    }
    
    private void SetupNavigation()
    {
        var navigationService = Ioc.Default.GetRequiredService<INavigationService>();
        var assembly = Assembly.GetExecutingAssembly();
        navigationService.AppPages = assembly.DefinedTypes
            .Where(x => x.IsSubclassOf(typeof(UserControl)) || x.IsSubclassOf(typeof(Window)))
            .ToDictionary(typeInfo => typeInfo.Name, typeInfo => typeInfo.IsSubclassOf(typeof(Window))
                ? new Uri($"../Views/Windows/{typeInfo.Name}.xaml", UriKind.Relative)
                : typeInfo.IsSubclassOf(typeof(UserControl))
                    ? new Uri($"../Views/Pages/{typeInfo.Name}.xaml", UriKind.Relative)
                    : null)!;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MusicPlayer.App.WPF.Services.NavigationService;

[INotifyPropertyChanged]
public partial class NavigationService : INavigationService
{
    private readonly List<string> _historic;
    
    public Dictionary<string, Uri> AppPages { get; set; }

    [ObservableProperty] 
    private string _currentFrame;

    [ObservableProperty] 
    private string _currentPageKey;
    
    public object? Parameter { get; private set; }

    public NavigationService()
    {
        AppPages = new Dictionary<string, Uri>();
        _historic = new List<string>();
        CurrentFrame = "MainFrame";
    }
    
    public void GoBack()
    {
        if (!_historic.Any()) return;
        
        _historic.RemoveAt(_historic.Count - 1);
        NavigateTo(_historic.Last());
    }

    public void Configure(string key, Uri pageType)
    {
        lock (AppPages)
        {
            if (AppPages.ContainsKey(key))
            {
                AppPages[key] = pageType;
            }
            else
            {
                AppPages.Add(key, pageType);
            }
        }
    }

    public void NavigateTo(string pageKey)
    {
        NavigateTo(pageKey, null);
    }

    public void NavigateTo(string pageKey, object? parameter)
    {
        lock (AppPages)
        {
            if (!AppPages.ContainsKey(pageKey))
            {
                throw new ArgumentException($"No such page: {pageKey} ", nameof(pageKey));
            }

            if (System.Windows.Application.Current.MainWindow != null)
            {
                if (GetDescendantFromName(System.Windows.Application.Current.MainWindow, CurrentFrame) is Frame frame)
                {
                    frame.Source = AppPages[pageKey];
                }
            }

            Parameter = parameter;
            _historic.Add(pageKey);
            CurrentPageKey = pageKey;
        }
    }
    
    private static FrameworkElement? GetDescendantFromName(DependencyObject parent, string name)
    {
        var count = VisualTreeHelper.GetChildrenCount(parent);

        if (count < 1) return null;

        for (var i = 0; i < count; i++)
        {
            if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)
            {
                if (frameworkElement.Name == name)
                    return frameworkElement;

                frameworkElement = GetDescendantFromName(frameworkElement, name);
                if (frameworkElement != null)
                    return frameworkElement;
            }
        }
        return null;
    }
}
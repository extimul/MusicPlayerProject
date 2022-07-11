using System;
using System.Collections.Generic;

namespace MusicPlayer.App.WPF.Services.NavigationService;

public interface INavigationService
{
    Dictionary<string, Uri> AppPages { get; set; }
    
    string CurrentFrame { get; set; }
    
    string CurrentPageKey { get; }
    
    object? Parameter { get; }

    void GoBack();

    void Configure(string key, Uri pageType);
    
    void NavigateTo(string pageKey);

    void NavigateTo(string pageKey, object? parameter);
}
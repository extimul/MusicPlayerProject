using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace MusicPlayer.App.WPF.ViewModels;

public sealed class ViewModelLocator
{
    public static bool GetAutoWireViewModel(DependencyObject obj) {
        return (bool)obj.GetValue(AutoWireViewModelProperty);
    }

    public static void SetAutoWireViewModel(DependencyObject obj, bool value) {
        obj.SetValue(AutoWireViewModelProperty, value);
    }

    public static readonly DependencyProperty AutoWireViewModelProperty =
        DependencyProperty.RegisterAttached("AutoWireViewModel",
            typeof(bool), typeof(ViewModelLocator),
            new PropertyMetadata(false, OnAutoWireViewModelChanged)
        );
		
    private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if(DesignerProperties.GetIsInDesignMode(d)){
            return;
        }

        var assembly = Assembly.GetExecutingAssembly();
        var viewType = d.GetType();
        var typeName = viewType.FullName!.Replace(".Views.", ".ViewModels.") + "Model";;
        var viewModelTypeName = assembly.GetType(typeName);
        var viewModel = Activator.CreateInstance(viewModelTypeName);
	    
        ((FrameworkElement)d).DataContext = viewModel;
    }
}
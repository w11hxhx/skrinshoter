using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

public static class EditorButton
{
    public static readonly DependencyProperty KeyProperty = DependencyProperty.RegisterAttached("Key", typeof(string), typeof(EditorButton), new PropertyMetadata(null, OnKeyChanged));
    public static void SetKey(DependencyObject element, string value) => element.SetValue(KeyProperty, value);
    public static string GetKey(DependencyObject element) => (string)element.GetValue(KeyProperty);

    private static void OnKeyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is FrameworkElement element) element.Loaded += OnLoaded;
    }

    private static void OnLoaded(object sender, RoutedEventArgs args)
    {
        var element = (FrameworkElement)sender;
        element.Loaded -= OnLoaded;
        Apply(sender, args);
        var window = Window.GetWindow(element);
        if (window != null)
            window.IsVisibleChanged += (_, change) =>
            {
                if (change.NewValue is true) Apply(element, null);
            };
    }

    private static void Apply(object sender, RoutedEventArgs args)
    {
        var element = (FrameworkElement)sender;
        var settings = Program.Services.GetRequiredService<Settings>();
        var property = typeof(Settings).GetProperty("ShowEditor" + GetKey(element));
        element.Visibility = property?.GetValue(settings) is false ? Visibility.Collapsed : Visibility.Visible;
    }
}

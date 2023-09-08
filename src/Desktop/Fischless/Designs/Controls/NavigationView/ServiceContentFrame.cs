using ModernWpf.Controls;
using Serilog;
using System;
using System.Diagnostics;
using System.Windows;

namespace Fischless.Designs.Controls;

public class ServiceContentFrame : Frame
{
    public Type? ContentType
    {
        get => (Type)GetValue(ContentTypeProperty);
        set => SetValue(ContentTypeProperty, value);
    }

    public static readonly DependencyProperty ContentTypeProperty =
        DependencyProperty.Register(nameof(ContentType), typeof(Type), typeof(ServiceContentFrame), new PropertyMetadata(null!, OnContentTypeChanged));

    private static void OnContentTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ServiceContentFrame self)
        {
            self.LoadContent();
        }
    }

    public void LoadContent()
    {
        if (ContentType != null)
        {
            try
            {
                object page = AppConfig.GetService(ContentType) ?? throw new ArgumentNullException(nameof(ContentType));
                Navigate(page, null!);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                Debugger.Break();
            }
        }
    }
}

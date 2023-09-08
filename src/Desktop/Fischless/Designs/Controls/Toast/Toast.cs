using System.Windows;

namespace Fischless.Designs.Controls;

public static class Toast
{
    public static void Information(FrameworkElement owner, string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(owner, message, new ToastConfig(MessageBoxIcon.Info, location, offsetMargin, time));

    public static void Information(string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(null!, message, new ToastConfig(MessageBoxIcon.Info, location, offsetMargin, time));

    public static void Success(FrameworkElement owner, string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(owner, message, new ToastConfig(MessageBoxIcon.Success, location, offsetMargin, time));

    public static void Success(string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(null!, message, new ToastConfig(MessageBoxIcon.Success, location, offsetMargin, time));

    public static void Error(FrameworkElement owner, string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(owner, message, new ToastConfig(MessageBoxIcon.Error, location, offsetMargin, time));

    public static void Error(string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(null!, message, new ToastConfig(MessageBoxIcon.Error, location, offsetMargin, time));

    public static void Warning(FrameworkElement owner, string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(owner, message, new ToastConfig(MessageBoxIcon.Warning, location, offsetMargin, time));

    public static void Warning(string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(null!, message, new ToastConfig(MessageBoxIcon.Warning, location, offsetMargin, time));

    public static void Question(FrameworkElement owner, string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(owner, message, new ToastConfig(MessageBoxIcon.Question, location, offsetMargin, time));

    public static void Question(string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
        => Show(null!, message, new ToastConfig(MessageBoxIcon.Question, location, offsetMargin, time));

    public static void Ignore(FrameworkElement owner, string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
    {
    }

    public static void Ignore(string message, ToastLocation location = ToastLocation.TopCenter, Thickness offsetMargin = default, int time = ToastConfig.NormalTime)
    {
    }

    public static void Show(FrameworkElement owner, string message, ToastConfig options = null)
    {
        ToastControl toast = new(owner ?? Owner(), message, options);
        toast.ShowCore();
    }

    public static FrameworkElement Owner()
    {
        FrameworkElement owner = null!;

        foreach (Window window in Application.Current.Windows)
        {
            if (window.IsActive)
            {
                owner = window;
                break;
            }
        }
        return owner;
    }
}

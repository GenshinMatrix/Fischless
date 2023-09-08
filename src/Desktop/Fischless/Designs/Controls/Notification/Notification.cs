using Fischless.Native;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Fischless.Designs.Controls;

public static class Notification
{
    public static void AddNotice(string header, string title, string detail = null!, ToastDuration duration = ToastDuration.Short)
    {
        if (!OsVersionHelper.IsWindows10_OrGreater)
        {
            return;
        }

        new ToastContentBuilder()
            .AddHeader("AddNotice", header, "AddNotice")
            .AddText(title)
            .AddAttributionTextIf(!string.IsNullOrEmpty(detail), detail)
            .SetToastDuration(duration)
            .Show();
    }

    public static void AddNoticeWithButton(string header, string title, string button, (string, string) arg, ToastDuration duration = ToastDuration.Short)
    {
        if (!OsVersionHelper.IsWindows10_OrGreater)
        {
            return;
        }

        new ToastContentBuilder()
            .AddHeader("AddNotice", header, "AddNotice")
            .AddText(title)
            .AddButton(new ToastButton().SetContent(button).AddArgument(arg.Item1, arg.Item2).SetBackgroundActivation())
            .SetToastDuration(duration)
            .Show();
    }

    public static void ClearNotice()
    {
        if (!OsVersionHelper.IsWindows10_OrGreater)
        {
            return;
        }

        try
        {
            ToastNotificationManagerCompat.History.Clear();
        }
        catch
        {
        }
    }
}

file static class ToastContentBuilderExtensions
{
    public static ToastContentBuilder AddAttributionTextIf(this ToastContentBuilder builder, bool condition, string text)
    {
        if (condition)
        {
            return builder.AddAttributionText(text);
        }
        else
        {
            return builder;
        }
    }
}

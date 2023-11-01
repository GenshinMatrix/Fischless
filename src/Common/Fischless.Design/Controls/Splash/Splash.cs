using Fischless.Logging;
using Fischless.Threading;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Interop;
using Vanara.PInvoke;

namespace Fischless.Design.Controls;

public static class Splash
{
    public static STAThread<SplashWindow> Current { get; internal set; }

    static Splash()
    {
        if (!UriParser.IsKnownScheme("pack"))
            _ = PackUriHelper.UriSchemePack;
    }

    public static void ShowAsync(string imageUriString, string hint = null!, double opacity = 1d, Action completed = null!)
    {
        ShowAsync(new Uri(imageUriString), hint, opacity, completed);
    }

    public static void ShowAsync(Uri imageUri, string hint = null!, double opacity = 1d, Action completed = null!)
    {
        Current = new(sta =>
        {
            if (string.IsNullOrWhiteSpace(sta.Value.Name))
            {
                sta.Value.Name = "Splash Thread";
            }
            sta.Result = new SplashWindow(imageUri)
            {
                Hint = hint,
                Opacity = opacity,
            };
            sta.Result.Show();
        });
        Current.Start();

        if (completed != null)
        {
            _ = Task.Run(() =>
            {
                try
                {
                    Current.Value.Join();
                    completed?.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            });
        }
    }

    public static void CloseAsync(Window owner = null!, bool forced = false)
    {
        try
        {
            SplashWindow current = Current?.Result;

            if (current == null)
            {
                return;
            }
            current.Closing += (_, _) =>
            {
                owner.Dispatcher.Invoke(() =>
                {
                    nint hwnd = new WindowInteropHelper(owner).Handle;

                    _ = User32.SetForegroundWindow(hwnd);
                    _ = User32.BringWindowToTop(hwnd);
                });
            };
            if (forced)
            {
                current.Shutdown();
            }
            else
            {
                if (!current.AutoEnd)
                {
                    current.StartEnd();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}

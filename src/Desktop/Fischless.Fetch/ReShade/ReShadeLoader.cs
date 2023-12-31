﻿using Fischless.Design.Helpers;
using Fischless.Logging;
using Fischless.Native;
using Fischless.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Security.Principal;
using System.Text;
using Vanara.PInvoke;

namespace Fischless.Fetch.ReShade;

public static class ReShadeLoader
{
    public static void Launch(string loaderPath)
    {
        FluentProcess.Create()
            .FileName(Path.Combine(loaderPath, ReShadeSentimentalString.LoaderExe))
            .WorkingDirectory(loaderPath)
            .UseShellExecute()
            .Verb("runas")
            .Start()
            .Forget();
    }

    public static async Task LaunchAsync(string loaderPath, bool isSilent = false)
    {
        try
        {
            if (TryGetProcess(out Process?[] ps))
            {
                foreach (Process? p in ps)
                {
                    p?.Kill();
                }
            }
        }
        catch
        {
            throw;
        }

        FluentProcess loader = FluentProcess.Create()
            .FileName(Path.Combine(loaderPath, ReShadeSentimentalString.LoaderExe))
            .WorkingDirectory(loaderPath)
            .Verb("runas");

        if (isSilent)
        {
            _ = loader.CreateNoWindow()
                .UseShellExecute(false)
                .RedirectStandardOutput();
        }

        loader.Start();

        if (RuntimeHelper.IsElevated)
        {
            nint hWnd = await Task.Run(() =>
            {
                nint hWnd = IntPtr.Zero;

                if (SpinWaiter.SpinUntil(() => loader.MainWindowHandle != IntPtr.Zero, 3000))
                {
                    hWnd = loader.MainWindowHandle;
                }
                return hWnd;
            });
            if (hWnd != IntPtr.Zero)
            {
                if (ResourceHelper.GetStream("pack://application:,,,/Fischless;component/Assets/Icons/i2Q2r-4cr2K2kT3cSkf-ke.ico") is Stream iconStream)
                {
                    NativeMethods.ModifyWindowIcon(hWnd, new Icon(iconStream));
                    using (iconStream) _ = false;
                }
                if (isSilent)
                {
                    _ = User32.ShowWindow(hWnd, ShowWindowCommand.SW_HIDE);
                }
            }

            await Task.Delay(500);
        }
        else
        {
            await Task.Delay(500);

#if false // Not Elevated
            if (isSilent)
            {
                if (TryGetProcess(out Process?[] ps))
                {
                    foreach (Process? p in ps)
                    {
                        try
                        {
                            nint hWnd = p.MainWindowHandle;

                            if (hWnd != IntPtr.Zero)
                            {
                                if (ResourceHelper.GetStream("pack://application:,,,/Fischless;component/Assets/Icons/i2Q2r-4cr2K2kT3cSkf-ke.ico") is Stream iconStream)
                                {
                                    NativeMethods.ModifyWindowIcon(hWnd, new Icon(iconStream));
                                }

                                if (!User32.ShowWindow(hWnd, ShowWindowCommand.SW_HIDE))
                                {
                                    FluentProcess rundll32 = FluentProcess.Create()
                                       .FileName("rundll32")
                                       .Arguments($"{Lib.User32},{nameof(User32.ShowWindow)} {(int)hWnd}, {(int)ShowWindowCommand.SW_HIDE}")
                                       .WorkingDirectory(loaderPath)
                                       .Verb("runas")
                                       .CreateNoWindow()
                                       .UseShellExecute(false)
                                       .Start();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e);
                        }
                    }
                }
            }
#endif
        }
    }

    public static bool TryGetProcess(out Process?[] process)
    {
        try
        {
            Process[] ps = Process.GetProcessesByName("3DMigoto Loader");

            if (ps.Length > 0)
            {
                process = ps;
                return true;
            }
        }
        catch
        {
        }
        process = null!;
        return false;
    }

    public static void SetD3dxIniGameExe(string loaderPath, Func<string> setTarget)
    {
        if (setTarget == null)
        {
            return;
        }

        string d3dxIni = Path.Combine(loaderPath, ReShadeSentimentalString.LoaderD3dxIni);

        if (File.Exists(d3dxIni))
        {
            try
            {
                string text = File.ReadAllText(d3dxIni)?.Replace("\r", string.Empty);
                StringBuilder sb = new();

                foreach (string line in text.Split('\n'))
                {
                    if (line.StartsWith(ReShadeSentimentalString.LoaderD3dxIniGameExePrefix))
                    {
                        string target = line[ReShadeSentimentalString.LoaderD3dxIniGameExePrefix.Length..];

                        if (!File.Exists(target.Trim()))
                        {
                            target = setTarget.Invoke();

                            if (target == null)
                            {
                                return; // Canceled
                            }
                            sb.AppendLine($"{ReShadeSentimentalString.LoaderD3dxIniGameExePrefix}{target.Replace("/", @"\")}");
                            continue;
                        }
                    }
                    sb.AppendLine(line);
                }

                File.WriteAllText(d3dxIni, sb.ToString());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}

file static class RuntimeHelper
{
    public static bool IsElevated { get; } = GetElevated();

    private static bool GetElevated()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}

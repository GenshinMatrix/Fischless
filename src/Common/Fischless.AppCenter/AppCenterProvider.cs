using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.System;

namespace Fischless;

public static class AppCenterProvider
{
    public static string UserId { get; set; } = null!;
    public static string AppSecret { get; set; } = null!;

    public static bool IsUseAppCenter { get; set; } = false;
    public static bool IsStarted { get; private set; } = false;

    public static void Start(string userId, string appSecret)
    {
        IsUseAppCenter = true;
        UserId = userId;
        AppSecret = appSecret;
        AppCenter.SetUserId(UserId);
#if VERBOSE
        AppCenter.LogLevel = LogLevel.Verbose;
#endif
        AppCenter.Start(AppSecret, typeof(Analytics), typeof(Crashes));
        IsStarted = true;
    }

    public static void StartPrepare(string userId, string appSecret)
    {
        IsUseAppCenter = true;
        UserId = userId;
        AppSecret = appSecret;
    }

    public static void CompletePrepare()
    {
        if (IsUseAppCenter && !IsStarted)
        {
            Start(UserId, AppSecret);
        }
    }

    public static void TrackEvent(string name, params string?[] properties)
    {
        try
        {
            var dic = new Dictionary<string, string>();
            string? key = null;
            for (int i = 0; i < properties.Length; i++)
            {
                if (i % 2 == 0)
                {
                    key = properties[i];
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        dic[key!] = properties[i]!;
                    }
                }
            }
            Analytics.TrackEvent(name, dic);
        }
        catch (Exception e)
        {
            Crashes.TrackError(e, new Dictionary<string, string> { [nameof(MethodBase)] = nameof(AppCenterProvider) + nameof(TrackEvent) });
        }
    }

    public static void TrackError(Exception exception, IDictionary<string, string> properties = null!, params ErrorAttachmentLog[] attachments)
    {
        Crashes.TrackError(exception, properties, attachments);
    }

    public static void VisitWebsite(string owner = null!, string appName = null!, bool isOrgs = true)
    {
        try
        {
            if (string.IsNullOrEmpty(owner) || string.IsNullOrEmpty(appName))
            {
                _ = Launcher.LaunchUriAsync(new Uri("https://appcenter.ms"));
            }
            else
            {
                _ = Launcher.LaunchUriAsync(new Uri($"https://appcenter.ms/{(isOrgs ? "orgs" : "users")}/{owner}/apps/{appName}/crashes/errors"));
            }
        }
        catch
        {
        }
    }
}

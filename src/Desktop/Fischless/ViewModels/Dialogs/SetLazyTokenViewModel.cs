using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fischless.Design.Controls;
using Fischless.Design.Helpers;
using Fischless.Fetch.Lazy;
using Fischless.Helpers;
using Fischless.Threading;
using Fischless.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Fischless.ViewModels;

public partial class SetLazyTokenViewModel : ObservableObject
{
    [ObservableProperty]
    private string tokenInput = string.Empty;
    partial void OnTokenInputChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(TokenInput))
        {
            TokenOutput = string.Empty;
        }
        else
        {
            TokenOutput = LazyCrypto.Encrypt(TokenInput);
        }
    }

    [ObservableProperty]
    private string tokenOutput = string.Empty;

    public SetLazyTokenViewModel()
    {
        ReloadAsync().Forget();
    }

    private async Task ReloadAsync()
    {
        if (await LazyRepository.SetupToken())
        {
            string token = await LazyRepository.GetToken();

            if (RuntimeHelper.IsDebuggerAttached)
            {
                TokenInput = token;
            }
            TokenOutput = LazyCrypto.Encrypt(token);
        }
    }

    [RelayCommand]
    public async Task OpenTokenAsync()
    {
        FileOpenPicker dialog = new()
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.Desktop,
        };
        dialog.FileTypeFilter.Add(".txt");
        dialog.FileTypeFilter.Add(".token");

        InitializeWithWindow.Initialize(dialog, new WindowInteropHelper(ElementHelper.FindActivedWindow()).Handle);
        StorageFile file = await dialog.PickSingleFileAsync();

        if (file != null)
        {
            TokenInput = await File.ReadAllTextAsync(file.Path);
        }
    }

    [RelayCommand]
    public async Task TestTokenAsync()
    {
        if (string.IsNullOrWhiteSpace(TokenOutput))
        {
            return;
        }
        Stopwatch stopwatch = new();
        stopwatch.Start();
        string token = LazyCrypto.Decrypt(TokenOutput);
        if (!string.IsNullOrEmpty(await LazyRepository.GetFile(token)))
        {
            stopwatch.Stop();
            Notification.AddNotice(Mui("LazyTestTokenTitle"), Mui("LazyTestTokenPassHint", stopwatch.ElapsedMilliseconds));
        }
        else
        {
            Notification.AddNotice(Mui("LazyTestTokenTitle"), Mui("LazyTestTokenFailHint"));
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        try
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is SetLazyTokenDialog win)
                {
                    win.Close();
                    break;
                }
            }
        }
        catch
        {
        }
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(TokenInput))
        {
            Toast.Warning(Mui("LazyPleaseInputTokenHint"));
            return;
        }
        else if (!TokenInput.StartsWith("g"))
        {
            Toast.Warning(Mui("LazyTokenErrorHint"));
            return;
        }
        await LazyRepository.SaveToken(TokenInput);
        Cancel();
    }
}

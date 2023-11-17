using Fischless.Design.Controls;
using Fischless.Globalization;
using Fischless.Helpers;
using Fischless.Logging;
using Fischless.Plugin.RepairRegedit.ViewModel;
using Microsoft.Win32;
using ModernWpf.Controls;
using System.IO;
using System.Text;
using System.Windows;

namespace Fischless.Plugin.RepairRegedit.Views;

public partial class RepairRegeditContentDialog : ContentDialog
{
    public RepairRegeditViewModel ViewModel { get; }

    public RepairRegeditContentDialog()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }

    protected void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs e)
    {
        e.Cancel = true;

        try
        {
            SaveFileDialog dialog = new()
            {
                RestoreDirectory = true,
                FileName = "Fischless-RepairRegedit.reg",
                DefaultExt = "*.reg",
                Filter = "Register (*.reg)|*.reg",
            };

            if (dialog.ShowDialog() ?? false)
            {
                File.WriteAllText(dialog.FileName, ViewModel.ToRegFileText(), Encoding.Unicode);
                Toast.Information("操作成功");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            _ = MessageBoxX.Error(ex.ToString());
        }
    }

    private void OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        args.Cancel = true;

        if (!RuntimeHelper.IsElevated)
        {
            if (MessageBoxX.Question(MuiLanguage.Mui("UACRequestRestartHint")) == MessageBoxResult.Yes)
            {
                RuntimeHelper.RestartAsElevated();
            }
            return;
        }

        bool result = ViewModel.Save();

        if (result)
        {
            _ = MessageBoxX.Info("操作成功");
        }
        else
        {
            _ = MessageBoxX.Error("操作失败");
        }
    }

    private void OnCloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        ///
    }
}

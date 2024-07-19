using Fischless.SpaceX.Core;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Fischless.SpaceX;

public partial class MainWindow : Window
{
    private readonly Keyboard _keyboardHook = new();

    public MainWindow()
    {
        InitializeComponent();
        _keyboardHook.InstallHook(OnKeyPress);
    }

    private void OnKeyPress(Keyboard.HookStruct hookStruct, out bool handle)
    {
        handle = false;

        if (hookStruct.vkCode != (int)Keys.Space || hookStruct.flags != 0)
        {
            return;
        }

        (bool ok, string filePath) = ExplorerFile.GetCurrentFilePath();

        if (ok)
        {
            DirectoryInfo directoryInfo = new(filePath);

            if (Directory.Exists(filePath))
            {
                string[] filePaths = Directory.GetFiles(filePath);

                if (!filePaths.Any(f => f.EndsWith(".ini", StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }

                if (directoryInfo.Name.IsDisabledFolderPath())
                {
                    ReShade.Enable(filePath);
                }
                else
                {
                    ReShade.Disable(filePath);
                }
            }
        }
    }
}

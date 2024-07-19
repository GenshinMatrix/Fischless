using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace Fischless.SpaceX.Core;

internal class ExplorerFile
{
    [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
    static extern nint GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int GetClassName(nint hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern int GetWindowText(nint hWnd, StringBuilder text, int count);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern nint FindWindowEx(nint parentHandle, nint childAfter, string className, string? windowTitle);

    [DllImport("shlwapi.dll")]
    static extern int IUnknown_QueryService(nint pUnk, ref Guid guidService, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

    [DllImport("user32.dll")]
    public static extern bool GetGUIThreadInfo(uint tId, out GUITHREADINFO threadInfo);

    [StructLayout(LayoutKind.Sequential)]
    public struct GUITHREADINFO
    {
        public int cbSize;
        public int flags;
        public nint hwndActive;
        public nint hwndFocus;
        public nint hwndCapture;
        public nint hwndMenuOwner;
        public nint hwndMoveSize;
        public nint hwndCaret;
        public System.Drawing.Rectangle rcCaret;
    }

    private static readonly Guid ClsidShellWindows = new("9BA05972-F6A8-11CF-A442-00A0C90A8F39");
    private static readonly Guid SID_STopLevelBrowser = new(1284947520u, 37212, 4559, 153, 211, 0, 170, 0, 74, 232, 55);
    private static readonly Guid IID_IShellBrowser = new("000214E2-0000-0000-C000-000000000046");

    private static nint FindChildWindow(nint parentHandle, string className, string? windowTitle = null)
    {
        return FindWindowEx(parentHandle, IntPtr.Zero, className, windowTitle);
    }

    static string GetWindowText(nint handle)
    {
        const int nChars = 256;
        StringBuilder buffer = new(nChars);
        if (GetWindowText(handle, buffer, nChars) > 0)
        {
            return buffer.ToString();
        }
        return string.Empty;
    }

    static string GetClassName(nint handle)
    {
        const int nChars = 256;
        StringBuilder buffer = new(nChars);
        if (GetClassName(handle, buffer, nChars) > 0)
        {
            return buffer.ToString();
        }
        return string.Empty;
    }

    static void SendCopy()
    {
        Keyboard.INPUT[] inputs = new Keyboard.INPUT[1];
        inputs[0].type = Keyboard.INPUT_KEYBOARD;

        inputs[0].inputUnion.keyboardInput.wVk = Keyboard.VK_CONTROL;
        inputs[0].inputUnion.keyboardInput.dwFlags = Keyboard.KEYEVENTF_KEYDOWN;
        _ = Keyboard.SendInput(1, inputs, Marshal.SizeOf(typeof(Keyboard.INPUT)));

        inputs[0].inputUnion.keyboardInput.wVk = Keyboard.VK_C;
        inputs[0].inputUnion.keyboardInput.dwFlags = Keyboard.KEYEVENTF_KEYDOWN;
        _ = Keyboard.SendInput(1, inputs, Marshal.SizeOf(typeof(Keyboard.INPUT)));

        inputs[0].inputUnion.keyboardInput.wVk = Keyboard.VK_C;
        inputs[0].inputUnion.keyboardInput.dwFlags = Keyboard.KEYEVENTF_KEYUP;
        _ = Keyboard.SendInput(1, inputs, Marshal.SizeOf(typeof(Keyboard.INPUT)));

        inputs[0].inputUnion.keyboardInput.wVk = Keyboard.VK_CONTROL;
        inputs[0].inputUnion.keyboardInput.dwFlags = Keyboard.KEYEVENTF_KEYUP;
        _ = Keyboard.SendInput(1, inputs, Marshal.SizeOf(typeof(Keyboard.INPUT)));
    }

    public static (bool Ok, string FilePath) GetCurrentFilePath()
    {
        try
        {
            (nint activeWindowHandle, bool isDesktopWindow) = GetCurrentProcessInfo();
            if (activeWindowHandle == IntPtr.Zero) return (false, string.Empty);

            var selectedFils = isDesktopWindow ?
                GetSelectedFilesFromDesktop() :
                GetSelectedFilesFromFileExplorer(activeWindowHandle);
            if (selectedFils.Length > 0) return (true, selectedFils[0]);
        }
        catch (Exception)
        {
        }
        return (false, string.Empty);
    }

    private static string[] GetSelectedFilesFromFileExplorer(IntPtr foregroundWindowHandle)
    {
        dynamic shellWindows = Activator.CreateInstance(Type.GetTypeFromCLSID(ClsidShellWindows)!)!;
        foreach (dynamic webBrowserApp in shellWindows)
        {
            dynamic shellFolderView = webBrowserApp.Document;
            var folderTitle = shellFolderView.Folder.Title;
            if ((IntPtr)webBrowserApp.HWND != foregroundWindowHandle) continue;
            if (folderTitle != GetWindowText(foregroundWindowHandle)) continue;
            var selectedItems = shellFolderView.SelectedItems();
            string[] result = new string[selectedItems.Count];
            var i = 0;
            foreach (var item in selectedItems)
            {
                result[i++] = item.Path;
            }
            return result;
        }
        return [];
    }

    private static string[] GetSelectedFilesFromDesktop()
    {
        //IDataObject originalData = Clipboard.GetDataObject();

        try
        {
            SendCopy();
            Thread.Sleep(100);

            IDataObject dataObject = Clipboard.GetDataObject();

            if (dataObject.GetData(DataFormats.FileDrop, false) is string[] value)
            {
                return value;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
        finally
        {
            //if (originalData != null)
            //{
            //    Clipboard.SetDataObject(originalData);
            //}
        }

        return [];
    }

    private static string[] GetSelectedFilesFromShellBrowser(dynamic shellBrowser, bool onlySelectedFiles)
    {
        var shellView = shellBrowser.QueryActiveShellView();
        if (shellView != null)
        {
            const uint SVGIO_SELECTION = 2;
            const uint SVGIO_ALLVIEW = 0xFFFFFFFF;
            var selectionFlag = onlySelectedFiles ? SVGIO_SELECTION : SVGIO_ALLVIEW;
            shellView.ItemCount(selectionFlag, out int countItems);
            if (countItems > 0)
            {
                Guid IID_IShellItemArray = new("b63ea76d-1f85-456f-a19c-48159efa858b");
                shellView.Items(selectionFlag, IID_IShellItemArray, out dynamic items);
                var result = new string[countItems];
                for (int i = 0; i < countItems; i++)
                {
                    result[i] = items.GetItemAt(i).ToIFileSystemItem().Path;
                }
                return result;
            }
        }
        return [];
    }

    private static void RunAsSTA(Action threadStart)
    {
        try
        {
            Thread t = new(new ThreadStart(threadStart));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
        catch (Exception)
        {
        }
    }

    private static readonly string cabinetWClass = "cabinetwclass";

    private static readonly HashSet<string> ExplorerWindowClassNames = new() { cabinetWClass, "workerw", "progman" };

    private static readonly HashSet<string> ExplorerFocusClassNames = new() { "directuihwnd", "syslistview32" };

    private static bool IsDesktopWindow(string className)
    {
        return className != cabinetWClass;
    }

    private static (nint, bool) GetCurrentProcessInfo()
    {
        nint foregroundWindowHandle = GetForegroundWindow();
        string windowClassName = GetClassName(foregroundWindowHandle);
        if (!ExplorerWindowClassNames.Contains(windowClassName.ToLower()))
        {
            return (IntPtr.Zero, false);
        }

        GUITHREADINFO guiInfo = new();
        guiInfo.cbSize = Marshal.SizeOf(guiInfo);
        GetGUIThreadInfo(0, out guiInfo);
        nint focusedHandle = guiInfo.hwndFocus;
        string focusClassName = GetClassName(focusedHandle);

        if (!ExplorerFocusClassNames.Contains(focusClassName.ToLower()))
        {
            return (IntPtr.Zero, false);
        }

        return (foregroundWindowHandle, IsDesktopWindow(windowClassName));
    }
}

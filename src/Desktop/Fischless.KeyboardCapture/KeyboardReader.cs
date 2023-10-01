using System.Diagnostics;

namespace Fischless.KeyboardCapture;

public class KeyboardReader : IDisposable
{
    public static KeyboardReader Default { get; } = new();

    public event EventHandler<KeyboardResult> Received = null!;

    public bool IsCaseSensitived = false;
    protected KeyboardHook KeyboardHook = new();
    protected bool IsShift = false;
    protected bool IsCtrl = false;
    protected bool IsAlt = false;
    protected bool IsWin = false;

    public KeyboardReader()
    {
        Start();
    }

    ~KeyboardReader()
    {
        Dispose();
    }

    public void Dispose()
    {
        Stop();
    }

    public void Start()
    {
        KeyboardHook.KeyDown -= OnKeyDown;
        KeyboardHook.KeyDown += OnKeyDown;
        KeyboardHook.KeyUp -= OnKeyUp;
        KeyboardHook.KeyUp += OnKeyUp;
        KeyboardHook.Start();
    }

    public void Stop()
    {
        KeyboardHook.Stop();
        KeyboardHook.KeyDown -= OnKeyDown;
        KeyboardHook.KeyUp -= OnKeyUp;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Shift
         || e.KeyCode == Keys.ShiftKey
         || e.KeyCode == Keys.LShiftKey
         || e.KeyCode == Keys.RShiftKey)
        {
            IsShift = true;
            return;
        }
        else if (e.KeyCode == Keys.Control
              || e.KeyCode == Keys.ControlKey
              || e.KeyCode == Keys.LControlKey
              || e.KeyCode == Keys.RControlKey)
        {
            IsCtrl = true;
            return;
        }
        else if (e.KeyCode == Keys.LWin
              || e.KeyCode == Keys.RWin)
        {
            IsWin = true;
        }
        else if (e.KeyCode == Keys.Alt
              || e.KeyCode == Keys.LMenu
              || e.KeyCode == Keys.RMenu)
        {
            IsAlt = true;
        }

        var now = DateTime.Now;

        Debug.WriteLine(e.KeyCode);

        KeyboardItem item;

        if (IsCaseSensitived)
        {
            bool isUpper = Control.IsKeyLocked(Keys.CapsLock) ? !IsShift : IsShift;

            if (isUpper)
            {
                item = new(now, e.KeyCode, char.ToUpper((char)e.KeyCode).ToString());
            }
            else
            {
                item = new(now, e.KeyCode, char.ToLower((char)e.KeyCode).ToString());
            }
        }
        else
        {
            item = new(now, e.KeyCode);
        }

        KeyboardResult result = new(item)
        {
            IsShift = IsShift,
            IsCtrl = IsCtrl,
            IsAlt = IsAlt,
            IsWin = IsWin,
        };

        Received?.Invoke(this, result);
        Debug.WriteLine(result.ToString());
    }

    private void OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Shift
         || e.KeyCode == Keys.ShiftKey
         || e.KeyCode == Keys.LShiftKey
         || e.KeyCode == Keys.RShiftKey)
        {
            IsShift = false;
            return;
        }
        else if (e.KeyCode == Keys.Control
              || e.KeyCode == Keys.ControlKey
              || e.KeyCode == Keys.LControlKey
              || e.KeyCode == Keys.RControlKey)
        {
            IsCtrl = false;
            return;
        }
        else if (e.KeyCode == Keys.LWin
              || e.KeyCode == Keys.RWin)
        {
            IsWin = false;
            return;
        }
        else if (e.KeyCode == Keys.Alt
              || e.KeyCode == Keys.LMenu
              || e.KeyCode == Keys.RMenu)
        {
            IsAlt = false;
            return;
        }
    }
}

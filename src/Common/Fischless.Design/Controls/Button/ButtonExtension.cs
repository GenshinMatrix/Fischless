using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

namespace Fischless.Design.Controls;

public static class ButtonExtension
{
    public static void SendClick(this Button self)
    {
        if (new ButtonAutomationPeer(self).GetPattern(PatternInterface.Invoke) is IInvokeProvider invokeProv)
        {
            invokeProv?.Invoke();
        }
    }
}

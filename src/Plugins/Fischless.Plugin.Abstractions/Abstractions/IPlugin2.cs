using System.Windows.Input;

namespace Fischless.Plugin.Abstractions;

public interface IPlugin2
{
    public bool IsShowButton { get; }
    public ICommand ButtonCommand { get; }
}

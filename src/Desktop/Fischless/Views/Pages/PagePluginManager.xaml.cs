using Fischless.ViewModels;
using ModernWpf.Controls;

namespace Fischless.Views;

public partial class PagePluginManager : Page
{
    public PagePluginManagerViewModel ViewModel { get; }

    public PagePluginManager()
    {
        DataContext = ViewModel = new();
        InitializeComponent();
    }
}

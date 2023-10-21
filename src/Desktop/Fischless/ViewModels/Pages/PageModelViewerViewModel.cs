using CommunityToolkit.Mvvm.ComponentModel;

namespace Fischless.ViewModels;

public partial class PageModelViewerViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string modelPath = null!;
    partial void OnModelPathChanged(string value)
    {
    }

    public void LoadModel(string modelPath)
    {
        ModelPath = modelPath;
    }
}

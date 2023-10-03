using CommunityToolkit.Mvvm.ComponentModel;
using Fischless.Fetch.Lazy;
using System.Collections.ObjectModel;

namespace Fischless.ViewModels;

public partial class ShowLazyViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<LazyInput> lazys = new();

    public void Reload(string file)
    {
        LazyInput[] lazyIns = LazyInputHelper.AnalysisFile(file);

        foreach (LazyInput lazyIn in lazyIns)
        {
            Lazys.Add(lazyIn);
        }
    }
}

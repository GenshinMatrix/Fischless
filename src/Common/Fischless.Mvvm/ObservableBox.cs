using CommunityToolkit.Mvvm.ComponentModel;

namespace Fischless.Mvvm;

public partial class ObservableBox<T> : ObservableObject
{
    [ObservableProperty]
    private object? value = null;

    public ObservableBox()
    {
    }

    public ObservableBox(object? value)
    {
        this.value = value;
    }

    public static implicit operator ObservableBox<T>(T value)
    {
        return new(value);
    }
}

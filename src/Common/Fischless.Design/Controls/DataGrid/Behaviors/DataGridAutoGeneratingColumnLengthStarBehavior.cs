using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;

namespace Fischless.Design.Controls;

public sealed class DataGridAutoGeneratingColumnLengthStarBehavior : Behavior<DataGrid>
{
    protected override void OnAttached()
    {
        AssociatedObject.AutoGeneratingColumn += OnLoaded;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.AutoGeneratingColumn -= OnLoaded;
        base.OnDetaching();
    }

    private void OnLoaded(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        e.Column.Width = new DataGridLength(1d, DataGridLengthUnitType.Star);
    }
}

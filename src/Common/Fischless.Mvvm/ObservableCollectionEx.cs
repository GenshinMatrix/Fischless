using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fischless.Mvvm;

public class ObservableCollectionEx<T> : ObservableCollection<T>
{
    public ObservableCollectionEx() : base()
    {
    }

    public ObservableCollectionEx(IEnumerable<T> collection) : base(collection)
    {
    }

    public ObservableCollectionEx(List<T> list) : base(list)
    {
    }

    protected virtual void RaisePropertyChanged(PropertyChangedEventArgs e)
        => OnPropertyChanged(e);

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        => RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));

    public virtual void AddRange(IEnumerable<T> range)
    {
        foreach (var item in range)
        {
            Items.Add(item);
        }

        OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public virtual void Reset(IEnumerable<T> range)
    {
        Items.Clear();
        AddRange(range);
    }
}

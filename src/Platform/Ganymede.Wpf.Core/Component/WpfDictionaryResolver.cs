using System.Collections;

namespace TheXDS.Ganymede.Component;

/// <summary>
/// Implements a custom <see cref="DictionaryVisualResolver{T}"/> for WPF that
/// can be set from within Xaml.
/// </summary>
public class WpfDictionaryResolver : DictionaryVisualResolver<FrameworkElement>, ICollection<ResolverEntry>
{
    /// <inheritdoc/>
    public int Count => Registry.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public void Add(ResolverEntry item)
    {
        Registry.Add(item.ViewModel, item.View);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Registry.Clear();
    }

    /// <inheritdoc/>
    public bool Contains(ResolverEntry item)
    {
        return Registry.ContainsKey(item.ViewModel);
    }

    /// <inheritdoc/>
    public void CopyTo(ResolverEntry[] array, int arrayIndex)
    {
        for (var i = 0; i < Registry.Count; i++)
        {
            var pair = Registry.ElementAt(i);
            array[i + arrayIndex] = ToEntry(pair);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<ResolverEntry> GetEnumerator()
    {
        return Registry.Select(ToEntry).GetEnumerator();
    }

    /// <inheritdoc/>
    public bool Remove(ResolverEntry item)
    {
        return Registry.Remove(item.ViewModel);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private ResolverEntry ToEntry(KeyValuePair<Type, Type> pair)
    {
        return new ResolverEntry() { ViewModel = pair.Key, View = pair.Value };
    }
}
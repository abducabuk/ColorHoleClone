using System;
using System.Collections.Generic;

[System.Serializable]
public class ObservedList<T> : List<T>
{
    public event Action<int> OnItemChanged = delegate { };
    public event Action OnListUpdated = delegate { };

    public new void Add(T item)
    {
        base.Add(item);
        OnListUpdated();
    }
    public new void Remove(T item)
    {
        base.Remove(item);
        OnListUpdated();
    }
    public new void AddRange(IEnumerable<T> collection)
    {
        base.AddRange(collection);
        OnListUpdated();
    }
    public new void RemoveRange(int index, int count)
    {
        base.RemoveRange(index, count);
        OnListUpdated();
    }
    public new void Clear()
    {
        base.Clear();
        OnListUpdated();
    }
    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        OnListUpdated();
    }
    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        base.InsertRange(index, collection);
        OnListUpdated();
    }
    public new void RemoveAll(Predicate<T> match)
    {
        base.RemoveAll(match);
        OnListUpdated();
    }

    public new T this[int index]
    {
        get
        {
            return base[index];
        }
        set
        {
            base[index] = value;
            OnItemChanged(index);
        }
    }

}
using System.Collections.Generic;

[System.Serializable]
public class LRUCache<T> where T : class
{
    private List<T> _entries;
    private int _maxSize;

    public LRUCache(int size)
    {
        _entries = new List<T>();
        _maxSize = size;
    }

    public void SetSize(int size)
    {
        _maxSize = size;
    }

    public S FindEntryOfType<S>() where S : class
    {
        return _entries.Find(item => item.GetType() == typeof(S)) as S;
    }

    public T Find(T item)
    {
        return _entries.Find(entry => entry == item);
    }

    public bool IsCacheFull() => _entries.Count >= _maxSize;

    public T Access(T item)
    {
        var entry = Find(item);

        if(entry == null)
        {
            if (IsCacheFull())
            {
                //deleting last entry
                var lastIndex = _entries.Count - 1;
                _entries.RemoveAt(lastIndex);
            }
        }
        else
        {
            _entries.Remove(entry);
        }

        _entries.Insert(0, item);
        return item;
    }
}

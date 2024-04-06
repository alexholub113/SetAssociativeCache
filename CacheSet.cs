namespace SetAssociativeCache;

public class CacheSet<TKey, TValue>(int capacity, IReplacementAlgorithm<int> replacementAlgorithm)
    where TKey : notnull
{
    private int _currentSize = 0;
    private readonly CacheSetItem<TKey, TValue>?[] _items = new CacheSetItem<TKey, TValue>[capacity];

    public TValue? Get(TKey key)
    {
        var index = HashFunction(key);
        var initialIndex = index;

        // Use Linear Probing to solve collisions
        do
        {
            var item = _items[index];
            if (item != null && item.Key.Equals(key))
            {
                replacementAlgorithm.Add(index);
                return item.Value;
            }
            index = (index + 1) % capacity;
        } while (index != initialIndex);

        return default;
    }

    public void Add(TKey key, TValue value)
    {
        var index = HashFunction(key);
        var initialIndex = index;
        do
        {
            var item = _items[index];
            if (item == null || item.Key.Equals(key))
            {
                _items[index] = new CacheSetItem<TKey, TValue>(key, value);
                return;
            }
            index = (index + 1) % capacity;
        } while (index != initialIndex);

        // If the cache is full, remove the victim
        var victimIndex = replacementAlgorithm.Replace();
        _items[victimIndex] = new CacheSetItem<TKey, TValue>(key, value);
    }

    public bool ContainsKey(TKey key)
    {
        return Get(key) != null;
    }

    private int HashFunction(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % capacity;
    }
}
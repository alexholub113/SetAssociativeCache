namespace SetAssociativeCache;

public class SetAssociativeCache<TKey, TValue>(CacheSet<TKey, TValue>[] sets)
    where TKey : notnull
{
    public TValue? Get(TKey key)
    {
        return sets[GetSetIndex(key)].Get(key);
    }

    public void Add(TKey key, TValue value)
    {
        sets[GetSetIndex(key)].Add(key, value);
    }

    private int GetSetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % sets.Length;
    }
}
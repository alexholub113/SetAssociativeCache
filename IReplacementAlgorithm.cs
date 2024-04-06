namespace SetAssociativeCache;

public interface IReplacementAlgorithm<TKey>
{
    void Add(TKey key);
    TKey Replace();
}
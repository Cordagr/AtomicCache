public class PersistentLoader<TKey, TValue> : PersistentCacheLoader<TKey, TValue>
{
    private readonly ICachePersistence<TKey, TValue> _persistence;

    public PersistentLoader(ICachePersistence<TKey, TValue> persistence)
    {
        _persistence = persistence;
    }

    public Dictionary<TKey, CacheItem<TValue>> LoadCacheFromPersistence()
        => _persistence.loadCache();

    public void SaveCacheToPersistence(Dictionary<TKey, CacheItem<TValue>> cache)
        => _persistence.saveCache(cache);

    public void ClearCacheInPersistence()
        => _persistence.clearCache();
}

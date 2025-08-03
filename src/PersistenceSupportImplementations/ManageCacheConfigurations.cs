  public enum EvictionPolicy
    {
        LRU,
        FIFO,
        LFU,
        MRU
    }

public class CacheConfigManager<TKey, TValue> : ManageCacheConfigurations<TKey, TValue>
{
    private int _capacity;
    private EvictionPolicy _policy;
    private string _filePath;
    private TimeSpan _expirationInterval;

    public void SetCacheCapacity(int capacity) => _capacity = capacity;

    public int GetCacheCapacity() => _capacity;

    public int GetCachePercentageUsedCapacity() => 0; // Placeholder

    public void SetCacheEvictionPolicy(EvictionPolicy policy) => _policy = policy;

    public EvictionPolicy GetCacheEvictionPolicy() => _policy;

    public void SetCacheItemCleanupExpirationTimer(TimeSpan expirationTime)
        => _expirationInterval = expirationTime;

    public void setFilePath(string filePath) => _filePath = filePath;

    public void getCurrentFilePath(out string filePath) => filePath = _filePath;

    public void SaveInternalCache(Dictionary<TKey, CacheItem<TValue>> cache)
    {
       // var persistence = new JsonCachePersistence<TKey, TValue>;
        //persistence.saveCache(cache);
    }
}

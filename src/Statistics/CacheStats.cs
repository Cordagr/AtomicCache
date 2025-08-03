public class CacheStats
{
    private uint m_MissCount;
    private uint m_EvictionCount; 
    private uint m_EraseCount;
    private uint m_InvalidationCount; // Count of invalidation due to TTL expiration (automatic cleanup)

    public int TotalItems { get; set; }
    public int EvictedItems { get; set; }
    public TimeSpan TotalTimeInCache { get; set; }
    public DateTime LastAccessed { get; set; }

    public CacheStats(int totalItems, int evictedItems, TimeSpan totalTimeInCache, DateTime lastAccessed)
    {
        TotalItems = totalItems;
        EvictedItems = evictedItems;
        TotalTimeInCache = totalTimeInCache;
        LastAccessed = lastAccessed;
    }

    public override string ToString()
    {
        return $"Total Items: {TotalItems}, Evicted Items: {EvictedItems}, Total Time in Cache: {TotalTimeInCache}, Last Accessed: {LastAccessed}";
    }


}
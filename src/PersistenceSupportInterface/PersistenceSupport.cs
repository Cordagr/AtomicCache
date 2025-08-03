public interface ICachePersistence<TKey, TValue>
{
    // save cache to persistent storage 
    void saveCache(Dictionary<TKey, CacheItem<TValue>> cache);
    // loading cache from persistence storage
    Dictionary<TKey, CacheItem<TValue>> loadCache();
  
    void clearCache();
}

public interface CacheSnapshotManager<TKey, TValue>
{
    void TakeSnapshot(Dictionary<TKey, CacheItem<TValue>> cache);
    void RestoreSnapshot(Dictionary<TKey, CacheItem<TValue>> cache);
    void ScheduleSnapshot(string snapshotID, TimeSpan interval);
    void CancelScheduledSnapshot(string snapshotID);
}

public interface PersistentCacheLoader<Tkey, TValue>
{

    Dictionary<Tkey, CacheItem<TValue>> LoadCacheFromPersistence();

    void SaveCacheToPersistence(Dictionary<Tkey, CacheItem<TValue>> cache);

    void ClearCacheInPersistence();
}

public interface ManageCacheConfigurations<TKey, TValue>
{
    void SetCacheCapacity(int capacity);
    int GetCacheCapacity();
    int GetCachePercentageUsedCapacity();
    void SetCacheEvictionPolicy(EvictionPolicy policy);
    EvictionPolicy GetCacheEvictionPolicy();
    // Reoccuring expiation timer for clean up // 
    void SetCacheItemCleanupExpirationTimer(TimeSpan expirationTime);
    void setFilePath(string filePath);
    void getCurrentFilePath(out string filePath);
    void SaveInternalCache(Dictionary<TKey, CacheItem<TValue>> cache);

}
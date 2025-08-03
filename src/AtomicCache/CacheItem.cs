using System;

public interface ICacheItem
{
    DateTime Expiration { get; set; }
    bool IsExpired();
    void UpdateLastAccessed();
    DateTime GetLastAccessed();
}

public enum CacheItemPriority
{
    Low,
    Normal,
    High
}

public class CacheItem<T> : ICacheItem
{
    public delegate void CallbackAction<T>(T item);

    public T Value { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastAccessed { get; private set; }
    public DateTime Expiration { get; set; }
    public int HitCount { get; private set; } = 0;
    public CacheItemPriority Priority { get; set; } = CacheItemPriority.Normal;

    public CallbackAction<T> EvictionCallback { get; set;}
    // TODO: Custom constructor for support a user-defined callback when an item is evicted // 

    public CacheItem(T value, TimeSpan? ttl = null)
    {
        Value = value;
        CreatedAt = DateTime.UtcNow;
        LastAccessed = CreatedAt;
        Expiration = CreatedAt.Add(ttl ?? TimeSpan.FromMinutes(30)); // default TTL
    }
    public CacheItem(T value)
    {
        Value = value;
        LastAccessed = DateTime.Now;
    }

    public bool IsExpired() => DateTime.UtcNow > Expiration;

    public void ResetExpiration(TimeSpan? ttl = null)
    {
        Expiration = DateTime.UtcNow.Add(ttl ?? TimeSpan.FromMinutes(30));
    }

    public void UpdateLastAccessed()
    {
        LastAccessed = DateTime.UtcNow;
        HitCount++;
    }

    public DateTime GetLastAccessed() => LastAccessed;
}

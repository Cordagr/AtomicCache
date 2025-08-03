using System;
using System.Collections.Generic;
using System.Timers;

namespace AtomicProject
{
    public class CacheItem<TValue>
    {
        public TValue Value { get; }

        public CacheItem(TValue value)
        {
            Value = value;
        }
    }

    public class AtomicCache<TKey, TValue>
    {
        public Dictionary<TKey, CacheItem<TValue>> _cache;
        public Dictionary<TKey, CacheItem<TValue>> InternalCache => _cache;

        private readonly Dictionary<TKey, int> _frequencyMap;
        private readonly Dictionary<TKey, DateTime> _lastAccessedMap;

        private readonly int _maxSize;
        private readonly object _lock = new object();
        private System.Timers.Timer _timer;

        public event Action<TKey, TValue> notifyEviction;

        public AtomicCache(int maxSize, double expirationIntervalInSeconds)
        {
            _cache = new Dictionary<TKey, CacheItem<TValue>>();
            _frequencyMap = new Dictionary<TKey, int>();
            _lastAccessedMap = new Dictionary<TKey, DateTime>();
            _maxSize = maxSize;

            _timer = new System.Timers.Timer(expirationIntervalInSeconds * 1000);
            _timer.Elapsed += (sender, e) => ClearExpiredItems();
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void Add(TKey key, TValue value)
        {
            lock (_lock)
            {
                if (_cache.ContainsKey(key))
                {
                    _cache[key] = new CacheItem<TValue>(value);
                    _frequencyMap[key]++;
                    _lastAccessedMap[key] = DateTime.Now;
                }
                else
                {
                    if (_cache.Count >= _maxSize)
                    {
                        EvictLFU(); // Or use EvictLRU();
                    }

                    _cache[key] = new CacheItem<TValue>(value);
                    _frequencyMap[key] = 1;
                    _lastAccessedMap[key] = DateTime.Now;
                }
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_lock)
            {
                if (_cache.TryGetValue(key, out var cacheItem))
                {
                    value = cacheItem.Value;
                    _frequencyMap[key]++;
                    _lastAccessedMap[key] = DateTime.Now;
                    return true;
                }
                value = default!;
                return false;
            }
        }

        private void EvictLFU()
        {
            lock (_lock)
            {
                if (_cache.Count == 0) return;

                TKey lfuKey = default!;
                int minFreq = int.MaxValue;

                foreach (var kvp in _frequencyMap)
                {
                    if (kvp.Value < minFreq)
                    {
                        minFreq = kvp.Value;
                        lfuKey = kvp.Key;
                    }
                }

                if (lfuKey != null && _cache.TryGetValue(lfuKey, out var cacheItem))
                {
                    _cache.Remove(lfuKey);
                    _frequencyMap.Remove(lfuKey);
                    _lastAccessedMap.Remove(lfuKey);

                    Console.WriteLine($"Item with key {lfuKey} has been removed by LFU eviction strategy.");
                    notifyEviction?.Invoke(lfuKey, cacheItem.Value);
                }
            }
        }

        private void EvictLRU()
        {
            lock (_lock)
            {
                if (_cache.Count == 0) return;

                TKey lruKey = default!;
                DateTime oldestAccess = DateTime.MaxValue;

                foreach (var kvp in _lastAccessedMap)
                {
                    if (kvp.Value < oldestAccess)
                    {
                        oldestAccess = kvp.Value;
                        lruKey = kvp.Key;
                    }
                }

                if (lruKey != null && _cache.TryGetValue(lruKey, out var cacheItem))
                {
                    _cache.Remove(lruKey);
                    _frequencyMap.Remove(lruKey);
                    _lastAccessedMap.Remove(lruKey);

                    Console.WriteLine($"Item with key {lruKey} has been removed by LRU eviction strategy.");
                    notifyEviction?.Invoke(lruKey, cacheItem.Value);
                }
            }
        }

        private void ClearExpiredItems()
        {
            lock (_lock)
            {
                var now = DateTime.Now;
                var expiredKeys = new List<TKey>();

                foreach (var kvp in _lastAccessedMap)
                {
                    if ((now - kvp.Value).TotalMinutes > 5)
                    {
                        expiredKeys.Add(kvp.Key);
                    }
                }

                foreach (var key in expiredKeys)
                {
                    if (_cache.TryGetValue(key, out var cacheItem))
                    {
                        _cache.Remove(key);
                        _frequencyMap.Remove(key);
                        _lastAccessedMap.Remove(key);
                        Console.WriteLine($"Item with key {key} has expired.");
                        notifyEviction?.Invoke(key, cacheItem.Value);
                    }
                }
            }
        }

        public void StopExpirationTimer()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        public void RegisterEvictionCallback(Action<TKey, TValue> callback)
        {
            notifyEviction += callback;
        }
    }
}

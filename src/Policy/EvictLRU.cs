using System;
using System.Collections.Generic;

namespace Policy
{
    // A generic LRU eviction policy
    public class EvictLRU<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _cache;
        private Action<TKey, TValue> notifyEviction;

        public EvictLRU(Dictionary<TKey, TValue> cache, Action<TKey, TValue> notifyEvictionCallback)
        {
            _cache = cache;
            notifyEviction = notifyEvictionCallback;
        }

        public void Evict()
        {
            if (_cache.Count == 0)
                return;

            TKey lruKey = default;
            DateTime oldestAccess = DateTime.MaxValue;

            foreach (var kvp in _cache)
            {
                if (kvp.Value is ITrackAccess accessTrackedValue)
                {
                    if (accessTrackedValue.GetLastAccessed() < oldestAccess)
                    {
                        oldestAccess = accessTrackedValue.GetLastAccessed();
                        lruKey = kvp.Key;
                    }
                }
            }

            if (lruKey != null)
            {
                TValue evictedValue = _cache[lruKey];
                _cache.Remove(lruKey);
                _RemoveKeyFromFreqMap(lruKey); // You must define this method

                Console.WriteLine($"Item with key {lruKey} has been removed by LRU eviction strategy.");
                notifyEviction?.Invoke(lruKey, evictedValue);
            }
        }

        private void _RemoveKeyFromFreqMap(TKey key)
        {
            // You need to define this logic based on your frequency map design
        }
    }

    // Interface to ensure values can return their last access time
    public interface ITrackAccess
    {
        DateTime GetLastAccessed();
    }
}

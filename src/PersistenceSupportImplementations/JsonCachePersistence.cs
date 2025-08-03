using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AtomicProject
{
    public class JsonCachePersistence<TKey, TValue>
    {
        private readonly string _filePath;

        public JsonCachePersistence(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveCache(Dictionary<TKey, CacheItem<TValue>> cache)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(cache, options);
            File.WriteAllText(_filePath, json);
        }

        public Dictionary<TKey, CacheItem<TValue>> LoadCache()
        {
            if (!File.Exists(_filePath))
                return new Dictionary<TKey, CacheItem<TValue>>();

            string json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<Dictionary<TKey, CacheItem<TValue>>>(json, options)
                   ?? new Dictionary<TKey, CacheItem<TValue>>();
        }

        public void ClearCache()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }
    }
}

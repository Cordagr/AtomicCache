namespace Policy
{
    public class EvictFIFO<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _cache;
        private Dictionary<TKey, int> _frequencyMap;

        public EvictFIFO(Dictionary<TKey, TValue> cache, Dictionary<TKey, int> frequencyMap)
        {
            _cache = cache;
            _frequencyMap = frequencyMap;
        }

        private void _RemoveKeyFromFreqMap(TKey key)
        {
            if (_frequencyMap.ContainsKey(key))
                _frequencyMap.Remove(key);
        }

        public void Evict()
        {
            if (_cache.Count == 0)
                return;

            var firstKey = _cache.Keys.GetEnumerator();
            if (firstKey.MoveNext())
            {
                TKey keyToRemove = firstKey.Current;
                _cache.Remove(keyToRemove);
                _RemoveKeyFromFreqMap(keyToRemove);
                Console.WriteLine($"Item with key {keyToRemove} has been removed by FIFO eviction strategy.");
            }
        }
    }
}

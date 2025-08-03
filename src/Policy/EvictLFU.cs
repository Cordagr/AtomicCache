namespace Policy
{
    public class EvictLFU<TKey, TValue>
    {
        private Dictionary<TKey, LinkedListNode<TKey>> _keyFrequency = new();
        private Dictionary<int, LinkedList<TKey>> _countMap = new();
        private Dictionary<TKey, TValue> _cache = new();
        private int _minCount = 1;

        public void Evict()
        {
            if (_countMap.TryGetValue(_minCount, out var keyList) && keyList.Count > 0)
            {
                var lfuKey = keyList.First.Value;
                keyList.RemoveFirst();

                _cache.Remove(lfuKey);
                _keyFrequency.Remove(lfuKey);
            }
        }

        // You would likely want methods to update _keyFrequency and _countMap elsewhere too
    }
}

using System;
using System.Collections.Generic;

namespace AtomicProject
{
    class Program
    {
        public static void Main()
        {
            var cache = new AtomicCache<string, string>(maxSize: 5, expirationIntervalInSeconds: 300);
            var snapshotManager = new SnapshotManager<string, CacheItem<string>>();

            var persistence = new JsonCachePersistence<string, string>("cache_snapshot.json");

            // restoring cache from disk
            var loadedSnapshot = persistence.LoadCache();
            snapshotManager.RestoreSnapshot(loadedSnapshot);

            cache.Add("A", "Apple");
            cache.Add("B", "Banana");
            cache.Add("C", "Cherry");

            // taking snapshot and persist store // 
            var currentSnapshot = cache.InternalCache;
            snapshotManager.TakeSnapshot(currentSnapshot);
            persistence.SaveCache(currentSnapshot);

            cache.Add("D", "Date");
            cache.Add("E", "Elderberry");
            cache.Add("F", "Fig");

            Console.WriteLine("Modified cache. Now restoring snapshot...");

            snapshotManager.RestoreSnapshot(currentSnapshot);

            if (cache.TryGetValue("B", out var val))
                Console.WriteLine($"Restored key B: {val}");

            if (!cache.TryGetValue("D", out _))
                Console.WriteLine("Key D not found after restore");

            snapshotManager.ScheduleSnapshot("testSnapshot", TimeSpan.FromSeconds(10), () => cache.InternalCache);

            Console.WriteLine("Waiting... press Enter to exit.");
            Console.ReadLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Timers;

public class SnapshotManager<TKey, TValue>
{
    private Dictionary<TKey, TValue> _snapshot;
    private readonly Dictionary<string, System.Timers.Timer> _scheduledSnapshots = new();

    public void TakeSnapshot(Dictionary<TKey, TValue> cache)
    {
        _snapshot = new Dictionary<TKey, TValue>(cache);
        Console.WriteLine("Snapshot taken.");
    }

    public void RestoreSnapshot(Dictionary<TKey, TValue> cache)
    {
        if (_snapshot == null)
        {
            Console.WriteLine("No snapshot to restore.");
            return;
        }

        cache.Clear();
        foreach (var kvp in _snapshot)
        {
            cache[kvp.Key] = kvp.Value;
        }
        Console.WriteLine("Snapshot restored.");
    }

    public void ScheduleSnapshot(string snapshotID, TimeSpan interval, Func<Dictionary<TKey, TValue>> getCache)
    {
        if (_scheduledSnapshots.ContainsKey(snapshotID))
            return;

        var timer = new System.Timers.Timer(interval.TotalMilliseconds);
        timer.Elapsed += (sender, e) => TakeSnapshot(getCache());
        timer.AutoReset = true;
        timer.Start();

        _scheduledSnapshots[snapshotID] = timer;
        Console.WriteLine($"Scheduled snapshot '{snapshotID}' every {interval.TotalSeconds} seconds.");
    }

    public void CancelScheduledSnapshot(string snapshotID)
    {
        if (_scheduledSnapshots.TryGetValue(snapshotID, out var timer))
        {
            timer.Stop();
            timer.Dispose();
            _scheduledSnapshots.Remove(snapshotID);
            Console.WriteLine($"Cancelled scheduled snapshot '{snapshotID}'.");
        }
    }
}

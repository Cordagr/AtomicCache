## Overview
Atomic Cache is a generic caching implementation which currently does not support threads but will in the future made in C# the following are supported. (Packaging in the future will also be added)

Implementation Mechanisms:
-  Automatic Expiration and Removal for cached items through time mechanisms or cache size exceeding 
-  Snapshot and restoration functionalities
-  Persistent disk storage (JSON formatted will support other formats in the future)
-  Supports multiple eviction strategies such as LFU,LRU,FIFO

# Installation
1. git clone https://github.com/Cordagr/AtomicProject
2. Build the project using .NET cli (dotnet build)

## How to use in a project (either options)
1. Add reference to project dot add reference ../AtomicCache/AtomicCache.csproj
2. Include manually source files in project itself

## Code Usage

## Basic Usage
``` csharp
// Initialize the cache with a max of 100 items and 60-second expiration
var cache = new AtomicCache<string, string>(maxSize: 100, expirationIntervalInSeconds: 60);
```

## Adding Items 
``` csharp
<cache.Add("username", "will123");
```

## Retrieving Items
``` csharp
if (cache.TryGetValue("username", out var name))
{
    Console.WriteLine($"Found: {name}");
}
```

## Removing Items
``` csharp
cache.Remove("username");
```

## Restoring from Snapshot
``` charp
snapshotManager.RestoreSnapshot(cache.InternalCache);
```

## Clearing Cache
```csharp
cache.Clear();
```

## Setting Built-in eviction policies
```csharp
cacheConfigManager.SetCacheEvictionPolicy(EvictionPolicy.FIFO); # Replace FIFO with any strategies listed below 
```

## The current implemented policies work as follow
| Replacement Policy | Defined in file           | Description                                                                |
| :----------------- | :------------------------ | :------------------------------------------------------------------------- |
| `Policy::FIFO`     | [`Cache/Policy/FIFO.h`]   | Works like a queue: the first element in is the first element out.         |
| `Policy::LFU`      | [`Cache/Policy/LFU.h`]    | Replaces the Least Frequently Used entry. (default if otherwise not stated)|
| `Policy::LRU`      | [`Cache/Policy/LRU.h`]    | Replaces the Least Recently Used entry.                                    |

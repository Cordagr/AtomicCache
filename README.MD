## Overview
Atomic Cache is a generic caching implementation which currently does not support threads but will in the future made in C# the following are supported. (Packaging in the future will also be added)

Implementation Mechanisms:
- ** Automatic Expiration and Removal for cached items through time mechanisms or cache size exceeding 
- ** Snapshot and restoration functionalities
- ** Persistent disk storage (JSON formatted will support other formats in the future)
- ** Supports multiple eviction strategies such as LFU,LRU,FIFO

## Example Usage

# Installation
1. git clone https://github.com/Cordagr/AtomicProject
2. Build the project using .NET cli (dotnet build)

## How to use in a project (either options)
1. Add reference to project dot add reference ../AtomicCache/AtomicCache.csproj
2. Include manually source files in project itself

## Code Usage

## Basic Usage
<pre lang="markdown"> ```csharp  
// Initialize the cache with a max of 100 items and 60-second expiration
var cache = new AtomicCache<string, string>(maxSize: 100, expirationIntervalInSeconds: 60);
``` </pre>

## Adding Items 
<pre lang ="markdown"> ```csharp
<cache.Add("username", "will123");
``` </pre>

## Retrieving Items
<pre lang="markdown"> ```csharp
if (cache.TryGetValue("username", out var name))
{
    Console.WriteLine($"Found: {name}");
}
``` </pre>

## Removing Items
<pre lang="markdown"> ```csharp
cache.Remove("username");
``` </pre>

## Restoring from Snapshot
<pre lang="markdown"> ```csharp
snapshotManager.RestoreSnapshot(cache.InternalCache);
``` </pre>

## Clearing Cache
<pre lang="markdown"> ```csharp
cache.Clear();
``` </pre>


## The current implemented policies work as follow
| Replacement Policy | Defined in file           | Description                                                                |
| :----------------- | :------------------------ | :------------------------------------------------------------------------- |
| `Policy::FIFO`     | [`Cache/Policy/FIFO.h`]   | Works like a queue: the first element in is the first element out.         |
| `Policy::LFU`      | [`Cache/Policy/LFU.h`]    | Replaces the Least Frequently Used entry. (default if otherwise not stated)|
| `Policy::LRU`      | [`Cache/Policy/LRU.h`]    | Replaces the Least Recently Used entry.                                    |

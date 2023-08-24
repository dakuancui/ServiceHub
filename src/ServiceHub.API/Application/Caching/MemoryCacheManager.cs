using System;
using Microsoft.Extensions.Caching.Memory;

namespace ServiceHub.API.Application.Caching
{
	//public class MemoryCacheManager : IMemoryCacheManager
	//{
 //       private readonly MemoryCache _cache; //MemoryCache is thread safe

 //       private readonly Dictionary<string, Task<Exception>> _refreshTaskList;

 //       private readonly SemaphoreSlim? _mutex = null;

 //       public MemoryCacheManager()
 //       {
 //           var memoryCacheOptions = new MemoryCacheOptions() { ExpirationScanFrequency = TimeSpan.MaxValue };
 //           _cache = new MemoryCache(memoryCacheOptions);

 //           _cache = new MemoryCache($"ServiceHubCache_{Guid.NewGuid()}");

 //           _refreshTaskList = new Dictionary<string, Task<Exception>>();

 //           _mutex = new SemaphoreSlim(1);
 //       }

 //       public Task<T> AddOrGetExistingAsync<T>(string cacheKeyName, Func<T, CancellationToken, Task<T>> contentProvider, int cacheItemTimeout = 0, bool returnStaleItem = true, Func<T, bool>? cacheItemValidator = null, Func<Exception, Task>? loggingProvider = null, int staleRefreshTimeoutMilliSeconds = 100)
 //       {
 //           throw new NotImplementedException();
 //       }
 //   }
}


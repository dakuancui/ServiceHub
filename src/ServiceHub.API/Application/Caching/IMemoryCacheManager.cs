using System;
namespace ServiceHub.API.Application.Caching
{
	public interface IMemoryCacheManager
	{
        Task<T> AddOrGetExistingAsync<T>(string cacheKeyName,
            Func<T, CancellationToken,
            Task<T>> contentProvider,
            int cacheItemTimeout = 0,
            bool returnStaleItem = true,
            Func<T, bool>? cacheItemValidator = null,
            Func<Exception, Task>? loggingProvider = null,
            int staleRefreshTimeoutMilliSeconds = 100);
    }
}


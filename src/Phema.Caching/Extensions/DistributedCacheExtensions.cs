using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	public static class DistributedCacheExtensions
	{
		public static void Set<TKey, TValue>(this IDistributedCache<TKey, TValue> cache, TKey key, TValue value)
		{
			cache.Set(key, value, new DistributedCacheEntryOptions());
		}

		public static Task SetAsync<TKey, TValue>(
			this IDistributedCache<TKey, TValue> cache,
			TKey key,
			TValue value,
			CancellationToken token = default)
		{
			return cache.SetAsync(key, value, new DistributedCacheEntryOptions(), token);
		}
	}
}
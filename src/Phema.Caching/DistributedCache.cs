using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Phema.Serialization;

namespace Phema.Caching
{
	internal sealed class DistributedCache<TValue> : DistributedCache<string, TValue>, IDistributedCache<TValue>
	{
		public DistributedCache(IDistributedCache cache, ISerializer serializer, IOptions<PhemaDistributedCacheOptions> options) 
			: base(cache, serializer, options)
		{
		}
	}
	
	internal class DistributedCache<TKey, TValue> : IDistributedCache<TKey, TValue>
	{
		private readonly ISerializer serializer;
		private readonly IDistributedCache cache;
		private readonly PhemaDistributedCacheOptions options;

		public DistributedCache(IDistributedCache cache, ISerializer serializer, IOptions<PhemaDistributedCacheOptions> options)
		{
			this.cache = cache;
			this.serializer = serializer;
			this.options = options.Value;
		}
		
		public TValue Get(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			var data = cache.Get(fullKey);
			
			return data == null
				? default
				: serializer.Deserialize<TValue>(data);
		}

		public async Task<TValue> GetAsync(TKey key, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			var data = await cache.GetAsync(fullKey, token);

			return data == null
				? default
				: serializer.Deserialize<TValue>(data);
		}

		public void Set(TKey key, TValue value)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var data = serializer.Serialize(value);

			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			cache.Set(
				key: fullKey,
				value: data,
				options: DistributedCacheHelper.GetOptions<TKey, TValue>(options));
		}

		public Task SetAsync(TKey key, TValue value, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var data = serializer.Serialize(value);

			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			return cache.SetAsync(
				key: fullKey,
				value: data, 
				options: DistributedCacheHelper.GetOptions<TKey, TValue>(options), 
				token: token);
		}

		public void Refresh(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			cache.Refresh(key: fullKey);
		}

		public Task RefreshAsync(TKey key, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			return cache.RefreshAsync(key: fullKey, token: token);
		}

		public void Remove(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			cache.Remove(key: fullKey);
		}

		public Task RemoveAsync(TKey key, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = DistributedCacheHelper.GetFullKey<TKey, TValue>(key, options);
			
			return cache.RemoveAsync(
				key: fullKey, 
				token: token);
		}
	}
}
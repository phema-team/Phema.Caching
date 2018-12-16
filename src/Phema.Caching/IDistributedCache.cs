using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	public interface IDistributedCache<TKey, TValue>
	{
		TValue Get(TKey key);
		Task<TValue> GetAsync(TKey key, CancellationToken token = default);

		void Set(TKey key, TValue value);
		Task SetAsync(TKey key, TValue value, CancellationToken token = default);

		void Refresh(TKey key);
		Task RefreshAsync(TKey key, CancellationToken token = default);

		void Remove(TKey key);
		Task RemoveAsync(TKey key, CancellationToken token = default);
	}
	
	internal class DistributedCache<TKey, TValue> : IDistributedCache<TKey, TValue>
	{
		private readonly IDistributedCache cache;
		private readonly DistributedCacheOptions options;

		public DistributedCache(IDistributedCache cache, IOptions<DistributedCacheOptions> options)
		{
			this.cache = cache;
			this.options = options.Value;
		}
		
		public TValue Get(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			var data = cache.Get(fullKey);
			
			return data == null
				? default
				: DistributedCacheHelper.Deserialize<TValue>(data, options);
		}

		public async Task<TValue> GetAsync(TKey key, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			var data = await cache.GetAsync(fullKey, token);

			return data == null
				? default
				: DistributedCacheHelper.Deserialize<TValue>(data, options);
		}

		public void Set(TKey key, TValue value)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var data = DistributedCacheHelper.Serialize(value, options);

			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			cache.Set(
				key: fullKey,
				value: data,
				options: DistributedCacheHelper.GetOptions<TValue>(options));
		}

		public Task SetAsync(TKey key, TValue value, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var data = DistributedCacheHelper.Serialize(value, this.options);

			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), this.options);
			
			return cache.SetAsync(
				key: fullKey,
				value: data, 
				options: DistributedCacheHelper.GetOptions<TValue>(options), 
				token: token);
		}

		public void Refresh(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			cache.Refresh(key: fullKey);
		}

		public Task RefreshAsync(TKey key, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			return cache.RefreshAsync(key: fullKey, token: token);
		}

		public void Remove(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			cache.Remove(key: fullKey);
		}

		public Task RemoveAsync(TKey key, CancellationToken token = default)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = DistributedCacheHelper.GetFullKey(key, typeof(TValue), options);
			
			return cache.RemoveAsync(
				key: fullKey, 
				token: token);
		}
	}
}
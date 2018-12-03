using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	using static DistributedCacheHelper;
	
	public abstract class DistributedCache<TKey, TValue>
	{
		internal IDistributedCache Cache { get; set; }
		internal DistributedCacheOptions Options { get; set; }

		protected TValue Get(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = GetFullKey(key, GetType(), Options);
			
			var data = Cache.Get(fullKey);
			
			return data == null
				? default
				: Deserialize<TValue>(data, Options);
		}
		
		protected async Task<TValue> GetAsync(TKey key, CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = GetFullKey(key, GetType(), Options);
			
			var data = await Cache.GetAsync(fullKey, token);

			return data == null
				? default
				: Deserialize<TValue>(data, Options);
		}

		protected void Set(
			TKey key,
			TValue value,
			DistributedCacheEntryOptions options = null)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var data = Serialize(value, Options);

			var fullKey = GetFullKey(key, GetType(), Options);
			
			Cache.Set(
				key: fullKey,
				value: data,
				options: options ?? new DistributedCacheEntryOptions());
		}
		
		protected Task SetAsync(
			TKey key,
			TValue value,
			DistributedCacheEntryOptions options = null,
			CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var data = Serialize(value, Options);

			var fullKey = GetFullKey(key, GetType(), Options);
			
			return Cache.SetAsync(
				key: fullKey,
				value: data, 
				options: options ?? new DistributedCacheEntryOptions(), 
				token: token);
		}

		protected void Refresh(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = GetFullKey(key, GetType(), Options);
			
			Cache.Refresh(key: fullKey);
		}
		
		protected Task RefreshAsync(TKey key, CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));

			var fullKey = GetFullKey(key, GetType(), Options);
			
			return Cache.RefreshAsync(key: fullKey, token: token);
		}

		protected void Remove(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = GetFullKey(key, GetType(), Options);
			
			Cache.Remove(key: fullKey);
		}

		protected Task RemoveAsync(TKey key, CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var fullKey = GetFullKey(key, GetType(), Options);
			
			return Cache.RemoveAsync(
				key: fullKey, 
				token: token);
		}
	}
}
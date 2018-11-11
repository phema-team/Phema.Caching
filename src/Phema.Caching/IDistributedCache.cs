using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	public interface IDistributedCache<TValue>
	{
		TValue Get(string key);
		Task<TValue> GetAsync(string key, CancellationToken token = new CancellationToken());
		void Set(string key, TValue value, DistributedCacheEntryOptions entryOptions);

		Task SetAsync(
			string key,
			TValue value,
			DistributedCacheEntryOptions entryOptions = null,
			CancellationToken token = new CancellationToken());

		void Refresh(string key);
		Task RefreshAsync(string key, CancellationToken token = new CancellationToken());
		void Remove(string key);
		Task RemoveAsync(string key, CancellationToken token = new CancellationToken());
	}
	
	internal class DistributedCache<TValue> : IDistributedCache<TValue>
	{
		private readonly IDistributedCache cache;
		private readonly DistributedCacheOptions options;

		public DistributedCache(IDistributedCache cache, IOptions<DistributedCacheOptions> options)
		{
			this.cache = cache;
			this.options = options.Value;
		}

		public TValue Get(string key)
		{
			var data = cache.Get(GetFullKey(key));

			if (data == null)
			{
				return default(TValue);
			}
			
			var value = options.Encoding.GetString(data);

			return JsonConvert.DeserializeObject<TValue>(value);
		}

		public async Task<TValue> GetAsync(string key, CancellationToken token = new CancellationToken())
		{
			var data = await cache.GetAsync(GetFullKey(key), token);
			
			if (data == null)
			{
				return default(TValue);
			}
			
			var value = options.Encoding.GetString(data);

			return JsonConvert.DeserializeObject<TValue>(value);
		}

		public void Set(string key, TValue value, DistributedCacheEntryOptions entryOptions)
		{
			var serializedValue = JsonConvert.SerializeObject(value);

			var data = options.Encoding.GetBytes(serializedValue);
			
			cache.Set(GetFullKey(key), data, entryOptions);
		}

		public Task SetAsync(string key, TValue value, 
			DistributedCacheEntryOptions entryOptions = null,
			CancellationToken token = new CancellationToken())
		{
			var serializedValue = JsonConvert.SerializeObject(value);

			var data = options.Encoding.GetBytes(serializedValue);

			return cache.SetAsync(
				GetFullKey(key), 
				data, 
				entryOptions ?? new DistributedCacheEntryOptions(), 
				token);
		}

		public void Refresh(string key)
		{
			cache.Refresh(GetFullKey(key));
		}

		public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
		{
			return cache.RefreshAsync(GetFullKey(key), token);
		}

		public void Remove(string key)
		{
			cache.Remove(GetFullKey(key));
		}

		public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
		{
			return cache.RemoveAsync(GetFullKey(key), token);
		}

		private string GetFullKey(string key)
		{
			var prefix = options.Prefixes[typeof(TValue)];

			return $"{prefix}:{key}";
		}
	}
}
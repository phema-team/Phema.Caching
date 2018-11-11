using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	public abstract class DistributedCache<TKey, TValue>
	{
		private readonly string prefix;

		protected DistributedCache()
		{
			var type = typeof(TValue);
			prefix = type.GetCustomAttribute<DataContractAttribute>()?.Name ?? type.Name;
		}
		
		internal IDistributedCache Cache { get; set; }

		protected async Task<TValue> GetAsync(TKey key, CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			var data = await Cache.GetAsync(GetFullKey(key), token);

			return data == null 
				? default 
				: JsonConvert.DeserializeObject<TValue>(Encoding.UTF8.GetString(data));
		}

		protected Task SetAsync(
			TKey key,
			TValue value,
			DistributedCacheEntryOptions entryOptions = null,
			CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			if (value == null)
				throw new ArgumentNullException(nameof(value));
			
			var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

			return Cache.SetAsync(GetFullKey(key), data, entryOptions ?? new DistributedCacheEntryOptions(), token);
		}

		protected Task RefreshAsync(TKey key, CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			return Cache.RefreshAsync(GetFullKey(key), token);
		}

		protected Task RemoveAsync(TKey key, CancellationToken token = new CancellationToken())
		{
			if (key == null)
				throw new ArgumentNullException(nameof(key));
			
			return Cache.RemoveAsync(GetFullKey(key), token);
		}
		
		private string GetFullKey(TKey key)
		{
			return $"{prefix}:{JsonConvert.SerializeObject(key)}";
		}
	}
	
	public class DistributedCache<TValue> : DistributedCache<string, TValue>
	{
	}
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	/// <summary>
	///   Represents a strongly-typed distributed cache
	/// </summary>
	public interface IDistributedCache<TKey, TValue>
	{
		/// <inheritdoc cref="IDistributedCache.Get"/>
		TValue Get(TKey key);

		/// <inheritdoc cref="IDistributedCache.GetAsync"/>
		Task<TValue> GetAsync(TKey key, CancellationToken token = default);

		/// <inheritdoc cref="IDistributedCache.Set"/>
		void Set(TKey key, TValue value, DistributedCacheEntryOptions options);

		/// <inheritdoc cref="IDistributedCache.SetAsync"/>
		Task SetAsync(TKey key, TValue value, DistributedCacheEntryOptions options, CancellationToken token = default);

		/// <inheritdoc cref="IDistributedCache.Refresh"/>
		void Refresh(TKey key);

		/// <inheritdoc cref="IDistributedCache.RefreshAsync"/>
		Task RefreshAsync(TKey key, CancellationToken token = default);

		/// <inheritdoc cref="IDistributedCache.Remove"/>
		void Remove(TKey key);

		/// <inheritdoc cref="IDistributedCache.RemoveAsync"/>
		Task RemoveAsync(TKey key, CancellationToken token = default);
	}

	/// <inheritdoc cref="IDistributedCache{TKey, TValue}"/>
	public interface IDistributedCache<TValue> : IDistributedCache<string, TValue>
	{
	}

	internal class DistributedCache<TKey, TValue> : IDistributedCache<TKey, TValue>
	{
		private readonly IDistributedCache cache;
		private readonly DistributedCacheOptions cacheOptions;

		// ReSharper disable once MemberCanBeProtected.Global
		public DistributedCache(IDistributedCache cache, IOptions<DistributedCacheOptions> cacheOptions)
		{
			this.cache = cache
				?? throw new ArgumentException(
					"No distributed cache specified. Check for Microsoft.Extensions.Caching.* packages");

			this.cacheOptions = cacheOptions.Value;
		}

		public TValue Get(TKey key)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			var data = cache.Get(key.ToString());

			return data is null
				? default
				: (TValue) cacheOptions.Deserializer(data, typeof(TValue));
		}

		public async Task<TValue> GetAsync(TKey key, CancellationToken token = default)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			var data = await cache.GetAsync(key.ToString(), token);

			return data is null
				? default
				: (TValue) cacheOptions.Deserializer(data, typeof(TValue));
		}

		public void Set(TKey key, TValue value, DistributedCacheEntryOptions options)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			if (value is null)
				throw new ArgumentNullException(nameof(value));

			var data = cacheOptions.Serializer(value);

			cache.Set(key.ToString(), data, options);
		}

		public Task SetAsync(
			TKey key,
			TValue value,
			DistributedCacheEntryOptions options,
			CancellationToken token = default)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			if (value is null)
				throw new ArgumentNullException(nameof(value));

			if (options is null)
				throw new ArgumentNullException(nameof(options));

			var data = cacheOptions.Serializer(value);

			return cache.SetAsync(key.ToString(), data, options, token);
		}

		public void Refresh(TKey key)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			cache.Refresh(key.ToString());
		}

		public Task RefreshAsync(TKey key, CancellationToken token = default)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			return cache.RefreshAsync(key.ToString(), token);
		}

		public void Remove(TKey key)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			cache.Remove(key.ToString());
		}

		public Task RemoveAsync(TKey key, CancellationToken token = default)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			return cache.RemoveAsync(key.ToString(), token);
		}
	}

	internal sealed class DistributedCache<TValue> : DistributedCache<string, TValue>, IDistributedCache<TValue>
	{
		public DistributedCache(IDistributedCache cache, IOptions<DistributedCacheOptions> cacheOptions) : base(cache, cacheOptions)
		{
		}
	}
}
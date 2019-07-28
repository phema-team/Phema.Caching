using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Phema.Serialization;

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
		private readonly ISerializer serializer;

		public DistributedCache(IDistributedCache cache, ISerializer serializer)
		{
			this.cache = cache
				?? throw new ArgumentException(
					"No distributed cache specified. Check for Microsoft.Extensions.Caching.* packages");

			this.serializer = serializer
				?? throw new ArgumentException("No serializer specified. Check for Phema.Serialization.* packages");
		}

		public TValue Get(TKey key)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			var data = cache.Get(key.ToString());

			return data is null
				? default
				: serializer.Deserialize<TValue>(data);
		}

		public async Task<TValue> GetAsync(TKey key, CancellationToken token = default)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			var data = await cache.GetAsync(key.ToString(), token);

			return data is null
				? default
				: serializer.Deserialize<TValue>(data);
		}

		public void Set(TKey key, TValue value, DistributedCacheEntryOptions options)
		{
			if (key is null)
				throw new ArgumentNullException(nameof(key));

			if (value is null)
				throw new ArgumentNullException(nameof(value));

			var data = serializer.Serialize(value);

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

			var data = serializer.Serialize(value);

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
		public DistributedCache(IDistributedCache cache, ISerializer serializer) : base(cache, serializer)
		{
		}
	}
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Caching
{
	public interface IDistributedCacheBuilder
	{
		/// <summary>
		/// Adds <see cref="IDistributedCache{TValue}"/> with string as key
		/// </summary>
		IDistributedCacheBuilder AddCache<TValue>();
		
		/// <summary>
		/// Adds <see cref="IDistributedCache{TKey,TValue}"/>
		/// </summary>
		IDistributedCacheBuilder AddCache<TKey, TValue>();
	}
}

namespace Phema.Caching.Internal
{
	internal sealed class DistributedCacheBuilder : IDistributedCacheBuilder
	{
		private readonly IServiceCollection services;

		public DistributedCacheBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		public IDistributedCacheBuilder AddCache<TValue>()
		{
			services.TryAddScoped<IDistributedCache<TValue>, DistributedCache<TValue>>();

			return this;
		}

		public IDistributedCacheBuilder AddCache<TKey, TValue>()
		{
			services.TryAddScoped<IDistributedCache<TKey, TValue>, DistributedCache<TKey, TValue>>();

			return this;
		}
	}
}
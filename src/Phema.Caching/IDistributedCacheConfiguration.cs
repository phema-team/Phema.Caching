using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	public interface IDistributedCacheConfiguration
	{
		IDistributedCacheConfiguration AddCache<TKey, TValue, TDistributedCache>(string prefix)
			where TDistributedCache : DistributedCache<TKey, TValue>;
	}
	
	internal class DistributedCacheConfiguration : IDistributedCacheConfiguration
	{
		private readonly IServiceCollection services;

		public DistributedCacheConfiguration(IServiceCollection services)
		{
			this.services = services;
		}
		
		public IDistributedCacheConfiguration AddCache<TKey, TValue, TDistributedCache>(string prefix)
			where TDistributedCache : DistributedCache<TKey, TValue>
		{
			services.Configure<DistributedCacheOptions>(options =>
				options.Prefixes.Add(typeof(TDistributedCache), prefix));

			services.AddScoped(InjectProperties<TKey, TValue, TDistributedCache>);
			return this;
		}

		private TDistributedCache InjectProperties<TKey, TValue, TDistributedCache>(IServiceProvider provider)
			where TDistributedCache : DistributedCache<TKey, TValue>
		{
			var instance = ActivatorUtilities.CreateInstance<TDistributedCache>(provider, Array.Empty<object>());
			instance.Cache = provider.GetRequiredService<IDistributedCache>();
			instance.Options = provider.GetRequiredService<IOptions<DistributedCacheOptions>>().Value;
			return instance;
		}
	}
}
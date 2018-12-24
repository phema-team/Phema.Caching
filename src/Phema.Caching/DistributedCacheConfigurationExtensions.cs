using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class DistributedCacheConfigurationExtensions
	{
		public static IDistributedCacheConfiguration AddCache<TKey, TValue>(
			this IDistributedCacheConfiguration configuration,
			string prefix,
			DistributedCacheEntryOptions options = null)
		{
			configuration.Services
				.Configure<DistributedCacheOptions>(o =>
				{
					o.Prefixes.Add(typeof(TValue), prefix);
					o.Options.Add(typeof(TValue), options ?? new DistributedCacheEntryOptions());
				});
			
			configuration.Services.AddScoped<IDistributedCache<TKey, TValue>, DistributedCache<TKey, TValue>>();
			
			return configuration;
		}
		
		public static IDistributedCacheConfiguration AddCache<TValue>(
			this IDistributedCacheConfiguration configuration,
			string prefix,
			DistributedCacheEntryOptions options = null)
		{
			configuration.Services
				.Configure<DistributedCacheOptions>(o =>
				{
					o.Prefixes.Add(typeof(TValue), prefix);
					o.Options.Add(typeof(TValue), options ?? new DistributedCacheEntryOptions());
				});
			
			configuration.Services.AddScoped<IDistributedCache<TValue>, DistributedCache<TValue>>();
			
			return configuration;
		}
	}
}
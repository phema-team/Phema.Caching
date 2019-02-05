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
				.Configure<PhemaDistributedCacheOptions>(o =>
				{
					var tuple = (typeof(TKey), typeof(TValue));
					
					o.Prefixes.Add(tuple, prefix);
					o.Options.Add(tuple, options ?? new DistributedCacheEntryOptions());
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
				.Configure<PhemaDistributedCacheOptions>(o =>
				{
					var tuple = (typeof(string), typeof(TValue));
					
					o.Prefixes.Add(tuple, prefix);
					o.Options.Add(tuple, options ?? new DistributedCacheEntryOptions());
				});
			
			configuration.Services.AddScoped<IDistributedCache<TValue>, DistributedCache<TValue>>();
			
			return configuration;
		}
	}
}
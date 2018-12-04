using System;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class DistributedRedisCacheExtensions
	{
		public static IServiceCollection AddDistributedRedisCaching(
			this IServiceCollection services, 
			Action<IDistributedCacheConfiguration> action)
		{
			services.AddDistributedRedisCache(options => {});
			return services.AddDistributedCaching(action);
		}
		
		public static IServiceCollection AddDistributedRedisCaching(
			this IServiceCollection services, 
			Action<IDistributedCacheConfiguration> action,
			Action<RedisCacheOptions> options)
		{
			services.AddDistributedRedisCache(options);
			return services.AddDistributedCaching(action);
		}
	}
}
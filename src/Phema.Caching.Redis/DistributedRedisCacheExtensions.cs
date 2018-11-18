using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class DistributedRedisCacheExtensions
	{
		public static IDistributedCacheBuilder AddDistributedRedisCaching(
			this IServiceCollection services, 
			Action<IDistributedCacheConfiguration> action)
		{
			var builder = services.AddDistributedCaching(action);
			
			services.AddSingleton<IDistributedCache, RedisCache>();
			services.ConfigureOptions<RedisCacheOptionsConfiguration>();
			
			return builder;
		}
	}
}
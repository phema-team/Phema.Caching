using System;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class DistributedRedisCacheExtensions
	{
		public static IServiceCollection AddPhemaDistributedRedisCache(
			this IServiceCollection services, 
			Action<IDistributedCacheConfiguration> configuration,
			Action<RedisCacheOptions> options = null)
		{
			return services
				.AddDistributedRedisCache(options ?? (o => { }))
				.AddPhemaDistributedCache(configuration);
		}
	}
}
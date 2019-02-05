using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching.Memory
{
	public static class DistributedMemoryCacheExtensions
	{
		public static IServiceCollection AddPhemaDistributedMemoryCache(
			this IServiceCollection services, 
			Action<IDistributedCacheConfiguration> configuration,
			Action<MemoryCacheOptions> options = null)
		{
			return services
				.AddDistributedMemoryCache(options ?? (o => {}))
				.AddPhemaDistributedCache(configuration);
		}
	}
}
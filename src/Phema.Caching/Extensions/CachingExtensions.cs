using System;
using Microsoft.Extensions.DependencyInjection;
using Phema.Caching.Internal;

namespace Phema.Caching
{
	public static class CachingExtensions
	{
		public static IServiceCollection AddDistributedCache(
			this IServiceCollection services,
			Action<IDistributedCacheBuilder> options)
		{
			options?.Invoke(new DistributedCacheBuilder(services));

			return services;
		}
	}
}
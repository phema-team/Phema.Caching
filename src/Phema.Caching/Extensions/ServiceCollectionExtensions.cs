using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Caching
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDistributedCache(
			this IServiceCollection services,
			Action<DistributedCacheOptions> options = null)
		{
			services.Configure(options ?? (o => {}));
			services.TryAddScoped(typeof(IDistributedCache<>), typeof(DistributedCache<>));
			services.TryAddScoped(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

			return services;
		}
	}
}
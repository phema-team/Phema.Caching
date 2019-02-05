using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPhemaDistributedCache(
			this IServiceCollection services, 
			Action<IDistributedCacheConfiguration> configuration)
		{
			configuration(new DistributedCacheConfiguration(services));

			return services;
		}
	}
}

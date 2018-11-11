using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class DistributedCacheExtensions
	{
		public static IServiceCollection AddDistributedCaching(this IServiceCollection services, Action<IDistributedCacheConfiguration> action)
		{
			services.AddOptions();
			
			action(new DistributedCacheConfiguration(services));
			
			return services;
		}
	}
}

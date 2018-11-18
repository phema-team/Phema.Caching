using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public static class DistributedCacheExtensions
	{
		public static IDistributedCacheBuilder AddDistributedCaching(this IServiceCollection services, Action<IDistributedCacheConfiguration> action)
		{
			action(new DistributedCacheConfiguration(services));
			
			return new DistributedCacheBuilder(services);
		}
	}
}

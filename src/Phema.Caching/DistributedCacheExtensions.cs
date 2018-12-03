using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Phema.Caching.Tests")]

namespace Phema.Caching
{
	public static class DistributedCacheExtensions
	{
		public static IServiceCollection AddDistributedCaching(this IServiceCollection services, Action<IDistributedCacheConfiguration> action)
		{
			action(new DistributedCacheConfiguration(services));

			return services;
		}
	}
}

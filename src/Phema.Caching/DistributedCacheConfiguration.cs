using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	internal sealed class DistributedCacheConfiguration : IDistributedCacheConfiguration
	{
		public DistributedCacheConfiguration(IServiceCollection services)
		{
			Services = services;
		}
		
		public IServiceCollection Services { get; }
	}
}
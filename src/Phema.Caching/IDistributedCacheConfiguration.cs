using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public interface IDistributedCacheConfiguration
	{
		IServiceCollection Services { get; }
	}
	
	internal class DistributedCacheConfiguration : IDistributedCacheConfiguration
	{
		public DistributedCacheConfiguration(IServiceCollection services)
		{
			Services = services;
		}
		
		public IServiceCollection Services { get; }
	}
}
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public interface IDistributedCacheBuilder
	{
		IServiceCollection Services { get; }
	}
	
	internal class DistributedCacheBuilder : IDistributedCacheBuilder
	{
		public DistributedCacheBuilder(IServiceCollection services)
		{
			Services = services;
		}

		public IServiceCollection Services { get; }
	}
}
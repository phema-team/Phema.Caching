using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public interface IDistributedCacheConfiguration
	{
		IServiceCollection Services { get; }
	}
}
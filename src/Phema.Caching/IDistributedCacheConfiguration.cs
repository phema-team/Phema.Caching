using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	public interface IDistributedCacheConfiguration
	{
		IDistributedCacheConfiguration AddCache<TKey, TValue, TDistributedCache>()
			where TDistributedCache : DistributedCache<TKey, TValue>;
	}
	
	internal class DistributedCacheConfiguration : IDistributedCacheConfiguration
	{
		private readonly IServiceCollection services;

		public DistributedCacheConfiguration(IServiceCollection services)
		{
			this.services = services;
		}
		
		public IDistributedCacheConfiguration AddCache<TKey, TValue, TDistributedCache>()
			where TDistributedCache : DistributedCache<TKey, TValue>
		{
			services.AddScoped<DistributedCache<TKey, TValue>, TDistributedCache>()
				.AddScoped(sp =>
				{
					var instance = sp.GetRequiredService<DistributedCache<TKey, TValue>>();
					instance.Cache = sp.GetRequiredService<IDistributedCache>();
					instance.Options = sp.GetRequiredService<DistributedCacheOptions>();
					return (TDistributedCache) instance;
				});
			
			return this;
		}
	}
}
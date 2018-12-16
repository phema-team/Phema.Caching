using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public interface IDistributedCacheConfiguration
	{
		IDistributedCacheConfiguration AddCache<TKey, TValue>(string prefix, DistributedCacheEntryOptions options = null);
	}
	
	internal class DistributedCacheConfiguration : IDistributedCacheConfiguration
	{
		private readonly IServiceCollection services;

		public DistributedCacheConfiguration(IServiceCollection services)
		{
			this.services = services;
		}
		
		public IDistributedCacheConfiguration AddCache<TKey, TValue>(string prefix, DistributedCacheEntryOptions options = null)
		{
			services.Configure<DistributedCacheOptions>(
				o =>
				{
					o.Prefixes.Add(typeof(TValue), prefix);
					o.Options.Add(typeof(TValue), options ?? new DistributedCacheEntryOptions());
				});
			services.AddScoped<IDistributedCache<TKey, TValue>, DistributedCache<TKey, TValue>>();
			
			return this;
		}
	}
}
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching
{
	public interface IDistributedCacheConfiguration
	{
		IDistributedCacheConfiguration AddCache<TValue>();
	}
	
	internal class DistributedCacheConfiguration : IDistributedCacheConfiguration
	{
		private readonly IServiceCollection services;

		public DistributedCacheConfiguration(IServiceCollection services)
		{
			this.services = services;
		}
		
		public IDistributedCacheConfiguration AddCache<TValue>()
		{
			services.Configure<DistributedCacheOptions>(
				options =>
				{
					var type = typeof(TValue);

					var attribute = type.GetCustomAttribute<DataContractAttribute>();

					var prefix = attribute?.Name ?? type.Name;
					
					options.Prefixes.Add(type, prefix);
				});

			services.AddSingleton<IDistributedCache<TValue>, DistributedCache<TValue>>();
			
			return this;
		}
	}
}
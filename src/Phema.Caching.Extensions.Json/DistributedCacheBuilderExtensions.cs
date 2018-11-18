using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	public static class DistributedCacheBuilderExtensions
	{
		public static IDistributedCacheBuilder WithJsonSerialization(this IDistributedCacheBuilder builder, Action<DistributedCacheJsonOptions> options = null)
		{
			if (options != null)
			{
				builder.Services.Configure(options);
			}
			
			builder.Services.AddSingleton<DistributedCacheOptions, DistributedCacheJsonOptions>(
				sp => sp.GetRequiredService<IOptions<DistributedCacheJsonOptions>>().Value);
			
			return builder;
		}
	}
}
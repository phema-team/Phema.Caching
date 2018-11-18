using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;

namespace Phema.Caching
{
	internal class RedisCacheOptionsConfiguration : IPostConfigureOptions<RedisCacheOptions>
	{
		private readonly RedisConfiguration configuration;

		public RedisCacheOptionsConfiguration(RedisConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void PostConfigure(string name, RedisCacheOptions options)
		{
			options.InstanceName = configuration.Name;
			options.Configuration = configuration.Configuration;
		}
	}
}
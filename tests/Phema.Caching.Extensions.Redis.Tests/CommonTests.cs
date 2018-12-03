using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using Xunit;

namespace Phema.Caching.Tests
{
	public class CommonTests
	{
		[Fact]
		public void AddsRedis()
		{
			var service = new ServiceCollection();
			
			service.AddDistributedRedisCaching(caching => {}, options => {});

			Assert.Single(service.Where(s => 
				s.ServiceType == typeof(IDistributedCache) 
				&& s.ImplementationType == typeof(RedisCache)));

			Assert.Single(service.Where(s =>
				s.ServiceType == typeof(IConfigureOptions<RedisCacheOptions>)));
		}
	}
}
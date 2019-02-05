using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Phema.Caching.Memory;
using Xunit;

namespace Phema.Caching.Tests
{
	public class CommonTests
	{
		[Fact]
		public void AddsRedis()
		{
			var service = new ServiceCollection();
			
			service.AddPhemaDistributedMemoryCache(caching => {}, options => {});

			Assert.Single(service.Where(s => 
				s.ServiceType == typeof(IDistributedCache) 
				&& s.ImplementationType == typeof(MemoryDistributedCache)));

			Assert.Single(service.Where(s =>
				s.ServiceType == typeof(IConfigureOptions<MemoryDistributedCacheOptions>)));
		}
	}
}
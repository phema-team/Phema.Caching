using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;
using Phema.Serialization;

namespace Phema.Caching.Tests
{
	public class DistributedCacheTests
	{
		[Fact]
		public void ServiceAddsCache()
		{
			var services = new ServiceCollection();

			services
				.AddJsonSerializer()
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching =>
					caching.AddCache<string, TestModel>("prefix"));

			Assert.Single(services.Where(s => s.ServiceType == typeof(IDistributedCache<string, TestModel>)));
		}
		
		[Fact]
		public async Task AddCache()
		{
			var services = new ServiceCollection();

			services
				.AddJsonSerializer()
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching =>
					caching.AddCache<string, TestModel>("prefix"));

			var provider = services.BuildServiceProvider();

			var memoryCache = provider.GetRequiredService<IDistributedCache>();
			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await cache.SetAsync("key", new TestModel { Name = "Alice"});

			Assert.NotNull(memoryCache.Get("prefix:key"));

			var model = await cache.GetAsync("key");
			
			Assert.Equal("Alice", model.Name);
		}
	}
}
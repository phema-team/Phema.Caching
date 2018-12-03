using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;

namespace Phema.Caching.Tests
{
	public class CommonTests
	{
		[Fact]
		public void ServiceAddsCache()
		{
			var services = new ServiceCollection();

			services.AddDistributedMemoryCache()
				.AddDistributedCaching(caching =>
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>("prefix"));

			Assert.Single(services.Where(s => s.ServiceType == typeof(TestModelDistributedCacheWithPublicMethods)));
		}
		
		[Fact]
		public async Task AddCache()
		{
			var services = new ServiceCollection();

			services.AddDistributedMemoryCache()
				.AddDistributedCaching(caching =>
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>("prefix"));

			var provider = services.BuildServiceProvider();

			var memoryCache = provider.GetRequiredService<IDistributedCache>();
			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await cache.SetAsync("key", new TestModel { Name = "Alice"});

			Assert.NotNull(memoryCache.Get("prefix:key"));

			var model = await cache.GetAsync("key");
			
			Assert.Equal("Alice", model.Name);
		}
	}
}
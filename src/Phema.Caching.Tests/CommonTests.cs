using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Phema.Caching.Tests
{
	public class CommonTests
	{
		private readonly IServiceCollection services;

		public CommonTests()
		{
			services = new ServiceCollection();
		}
		
		[Fact]
		public void AddsCaching()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			Assert.Single(services.Where(x => x.ServiceType == typeof(DistributedCache<string, TestModel>)));
			Assert.Single(services.Where(x => x.ServiceType == typeof(TestModelDistributedCacheWithPublicMethods)));
		}
		
		[Fact]
		public async Task SetNullKey()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.SetAsync(null, new TestModel()));
		}
		
		[Fact]
		public async Task SetNullValue()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.SetAsync("test", null));
		}
		
		[Fact]
		public async Task RemoveNullKey()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.RemoveAsync(null));
		}
		
		[Fact]
		public async Task RefreshNullKey()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.RefreshAsync(null));
		}
		
		[Fact]
		public async Task SetGetValue()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await cache.SetAsync("test", new TestModel { Name = "name"});

			var model = await cache.GetAsync("test");
			
			Assert.Equal("name", model.Name);
		}
		
		[Fact]
		public async Task SetRemoveGetValue()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await cache.SetAsync("test", new TestModel { Name = "name"});

			await cache.RemoveAsync("test");
			
			var model = await cache.GetAsync("test");
			
			Assert.Null(model);
		}
		
		[Fact]
		public async Task SetGetValueByPrefix()
		{
			services
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCacheWithPublicMethods>());

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<TestModelDistributedCacheWithPublicMethods>();
			
			await cache.SetAsync("test", new TestModel { Name = "name"});

			var distributedCache = provider.GetRequiredService<IDistributedCache>();

			var data = await distributedCache.GetAsync("TestModel:\"test\"");

			Assert.NotNull(data);
			
			var model = JsonConvert.DeserializeObject<TestModel>(Encoding.UTF8.GetString(data));
			
			Assert.Equal("name", model.Name);
		}
	}
}
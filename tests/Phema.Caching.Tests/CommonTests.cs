using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Phema.Serialization;

namespace Phema.Caching.Tests
{
	public class CommonTests
	{
		private readonly IServiceCollection services;

		public CommonTests()
		{
			services = new ServiceCollection();

			services
				.AddJsonSerializer()
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching =>
					caching.AddCache<string, TestModel>("test_model"));
		}
		
		[Fact]
		public void AddsCaching()
		{
			Assert.Single(services.Where(x => x.ServiceType == typeof(IDistributedCache<string, TestModel>)));
		}
		
		[Fact]
		public async Task SetNullKey()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.SetAsync(null, new TestModel()));
		}
		
		[Fact]
		public async Task SetNullValue()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.SetAsync("test", null));
		}
		
		[Fact]
		public async Task RemoveNullKey()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.RemoveAsync(null));
		}
		
		[Fact]
		public async Task RefreshNullKey()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await Assert.ThrowsAsync<ArgumentNullException>(
				async () => await cache.RefreshAsync(null));
		}
		
		[Fact]
		public async Task SetGetValue()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await cache.SetAsync("test", new TestModel { Name = "name"});

			var model = await cache.GetAsync("test");
			
			Assert.Equal("name", model.Name);
		}
		
		[Fact]
		public async Task SetRemoveGetValue()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await cache.SetAsync("test", new TestModel { Name = "name"});

			await cache.RemoveAsync("test");
			
			var model = await cache.GetAsync("test");
			
			Assert.Null(model);
		}
		
		[Fact]
		public async Task SetGetValueByPrefix()
		{
			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<string, TestModel>>();
			
			await cache.SetAsync("test", new TestModel { Name = "name"});

			var distributedCache = provider.GetRequiredService<IDistributedCache>();

			var data = await distributedCache.GetAsync("test_model:test");

			Assert.NotNull(data);
			
			var model = JsonConvert.DeserializeObject<TestModel>(Encoding.UTF8.GetString(data));
			
			Assert.Equal("name", model.Name);
		}
	}
}
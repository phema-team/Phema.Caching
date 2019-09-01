using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Caching.Tests
{
	public class TestModel
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}

	public class DistributedCacheTests
	{
		[Fact]
		public void SetGet()
		{
			var services = new ServiceCollection();

			services.AddDistributedMemoryCache()
				.AddDistributedCache();

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<TestModel>>();

			cache.Set("test", new TestModel {Name = "Test", Age = 12});

			var model = cache.Get("test");

			Assert.Equal("Test", model.Name);
			Assert.Equal(12, model.Age);
		}

		[Fact]
		public void SetRemoveGet()
		{
			var services = new ServiceCollection();

			services.AddDistributedMemoryCache()
				.AddDistributedCache();

			var provider = services.BuildServiceProvider();

			var cache = provider.GetRequiredService<IDistributedCache<TestModel>>();

			cache.Set("test", new TestModel());

			cache.Remove("test");

			var model = cache.Get("test");

			Assert.Null(model);
		}
	}
}
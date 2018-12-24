using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Caching.Tests
{
	public class StringDisctibutedKeyTests
	{
		[Fact]
		public void RegisterCache()
		{
			var services = new ServiceCollection();

			services.AddDistributedMemoryCache()
				.AddDistributedCaching(c => c.AddCache<TestModel>("test_model"));

			var provider = services.BuildServiceProvider();

			var cache = provider.GetService<IDistributedCache<TestModel>>();
			
			Assert.NotNull(cache);
			
			var model = new TestModel { Name = "name" };
			cache.Set("works", model);

			var result = cache.Get("works");
			
			Assert.Equal(model.Name, result.Name);
		}
	}
}
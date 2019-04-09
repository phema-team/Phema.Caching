using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;
using Phema.Serialization;

namespace Phema.Caching.Tests
{
	public class CachingExtensionsTests
	{
		[Fact]
		public void ServiceAddsCache()
		{
			var services = new ServiceCollection();

			services
				.AddDistributedMemoryCache()
				.AddDistributedCache(caching =>
					caching.AddCache<int, TestModel>()
						.AddCache<TestModel>())
				.AddNewtonsoftJsonSerializer();

			Assert.Single(services.Where(s => s.ServiceType == typeof(IDistributedCache<int, TestModel>)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(IDistributedCache<TestModel>)));
		}
	}
}
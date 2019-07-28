using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Phema.Serialization;
using Xunit;

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
				.AddDistributedCache()
				.AddNewtonsoftJsonSerializer();

			Assert.Single(services.Where(s => s.ServiceType == typeof(IDistributedCache<,>)));
			Assert.Single(services.Where(s => s.ServiceType == typeof(IDistributedCache<>)));
		}
	}
}
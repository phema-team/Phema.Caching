using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Phema.Serialization;
using Xunit;

namespace Phema.Caching.Tests
{
	public class CommunicationTests
	{
		[Fact]
		public void CommunicationWorks()
		{
			var services = new ServiceCollection();

			services
				.AddJsonSerializer()
				.AddDistributedMemoryCache()
				.AddDistributedCaching(caching =>
					caching.AddCache<string, TestModel>("prefix", new DistributedCacheEntryOptions
					{
						SlidingExpiration = TimeSpan.FromSeconds(1)
					}));

			services.Configure<DistributedCacheOptions>(o => o.Separator = ";;");
			
			var provider = services.BuildServiceProvider();

			provider.GetRequiredService<IDistributedCache<string, TestModel>>()
				.Set("key", new TestModel { Name = "name" });

			var data = provider.GetRequiredService<IDistributedCache>()
				.Get("prefix;;key");
			
			Assert.NotNull(data);

			var model = provider.GetRequiredService<IDistributedCache<string, TestModel>>()
				.Get("key");
			
			Assert.Equal("name", model.Name);
		}
	}
}
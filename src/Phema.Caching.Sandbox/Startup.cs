using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching.Sandbox
{
	public class TestModel
	{
		public string Name { get; set; }
	}
	
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => caching.AddCache<TestModel>());
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Run(async context =>
			{
				var cache = context.RequestServices.GetRequiredService<IDistributedCache<TestModel>>();

				var model = await cache.GetAsync("test");

				if (model == null)
				{
					model = new TestModel
					{
						Name = Guid.NewGuid().ToString("N")
					};
					
					await cache.SetAsync("test", model, new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
					});
				}

				await context.Response.WriteAsync(model.Name);
			});
		}
	}
}
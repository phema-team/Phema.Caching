using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Caching.Sandbox
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDistributedMemoryCache()
				.AddDistributedCaching(caching => 
					caching.AddCache<TestModel, TestModelDistributedCache>());

			services.AddScoped<StringGenerator>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Run(async context =>
			{
				var model = await context
					.RequestServices
					.GetRequiredService<TestModelDistributedCache>()
					.GetFromCacheOrCreate();

				await context.Response.WriteAsync(model.Name);
			});
		}
	}
}
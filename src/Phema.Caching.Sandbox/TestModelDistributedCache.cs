using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching.Sandbox
{
	public class TestModelDistributedCache : DistributedCache<TestModel>
	{
		private readonly StringGenerator generator;

		public TestModelDistributedCache(StringGenerator generator)
		{
			this.generator = generator;
		}
		
		public async Task<TestModel> GetFromCacheOrCreate()
		{
			var model = await GetAsync("test");

			if (model == null)
			{
				model = new TestModel
				{
					Name = generator.GetString()
				};
					
				await SetAsync("test", model, new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
				});
			}

			return model;
		}
	}
}
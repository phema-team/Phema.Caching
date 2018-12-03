using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace Phema.Caching.Tests
{
	public class DistributedCacheHelperTests
	{
		[Fact]
		public void GetFullKey()
		{
			var fullKey = DistributedCacheHelper.GetFullKey("key", typeof(DistributedCacheHelperTests), new DistributedCacheOptions
			{
				Prefixes =
				{
					[typeof(DistributedCacheHelperTests)] = "prefix"
				}
			});
			
			Assert.Equal("prefix:key", fullKey);
		}

		private class TestModel
		{
			public string Name { get; set; }
		}
		
		[Fact]
		public void Serialize()
		{
			var model = new TestModel { Name = "Alice" };

			var serialized = DistributedCacheHelper.Serialize(model, new DistributedCacheOptions());

			var expected = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
			
			Assert.Equal(expected, serialized);
		}
		
		[Fact]
		public void Derialize()
		{
			var options = new DistributedCacheOptions();
			var model = new TestModel { Name = "Alice" };
			
			var serialized = DistributedCacheHelper.Serialize(model, options);

			var deserialized = DistributedCacheHelper.Deserialize<TestModel>(serialized, options);
			
			Assert.Equal(model.Name, deserialized.Name);
		}
	}
}
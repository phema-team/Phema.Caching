using Xunit;

namespace Phema.Caching.Tests
{
	public class DistributedCacheHelperTests
	{
		[Fact]
		public void GetFullKey()
		{
			var tuple = (typeof(string), typeof(TestModel));
			
			var fullKey = DistributedCacheHelper.GetFullKey<string, TestModel>("key", new PhemaDistributedCacheOptions
			{
				Prefixes =
				{
					[tuple] = "prefix"
				}
			});
			
			Assert.Equal("prefix:key", fullKey);
		}
	}
}
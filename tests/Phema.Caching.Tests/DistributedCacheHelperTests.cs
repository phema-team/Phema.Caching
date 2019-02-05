using Xunit;

namespace Phema.Caching.Tests
{
	public class DistributedCacheHelperTests
	{
		[Fact]
		public void GetFullKey()
		{
			var fullKey = DistributedCacheHelper.GetFullKey("key", typeof(DistributedCacheHelperTests), new PhemaDistributedCacheOptions
			{
				Prefixes =
				{
					[typeof(DistributedCacheHelperTests)] = "prefix"
				}
			});
			
			Assert.Equal("prefix:key", fullKey);
		}
	}
}
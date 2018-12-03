using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching.Tests
{
	public class TestModelDistributedCacheWithPublicMethods : DistributedCache<TestModel>
	{
		public new Task<TestModel> GetAsync(string key, CancellationToken token = new CancellationToken())
		{
			return base.GetAsync(key, token);
		}
		
		public new Task SetAsync(
			string key,
			TestModel value,
			DistributedCacheEntryOptions entryOptions = null,
			CancellationToken token = new CancellationToken())
		{
			return base.SetAsync(key, value, entryOptions, token);
		}

		public new Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
		{
			return base.RefreshAsync(key, token);
		}

		public new Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
		{
			return base.RemoveAsync(key, token);
		}
	}
}
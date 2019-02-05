using System.Threading;
using System.Threading.Tasks;

namespace Phema.Caching
{
	public interface IDistributedCache<TKey, TValue>
	{
		TValue Get(TKey key);
		Task<TValue> GetAsync(TKey key, CancellationToken token = default);

		void Set(TKey key, TValue value);
		Task SetAsync(TKey key, TValue value, CancellationToken token = default);

		void Refresh(TKey key);
		Task RefreshAsync(TKey key, CancellationToken token = default);

		void Remove(TKey key);
		Task RemoveAsync(TKey key, CancellationToken token = default);
	}

	public interface IDistributedCache<TValue> : IDistributedCache<string, TValue>
	{
	}
}
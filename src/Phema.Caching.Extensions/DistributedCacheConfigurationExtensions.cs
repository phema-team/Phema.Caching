namespace Phema.Caching
{
	public static class DistributedCacheConfigurationExtensions
	{
		public static IDistributedCacheConfiguration AddCache<TModel, TDistributedCache>(
			this IDistributedCacheConfiguration caching)
			where TDistributedCache : DistributedCache<string, TModel>
		{
			return caching.AddCache<string, TModel, TDistributedCache>();
		}
	}
}
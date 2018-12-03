namespace Phema.Caching
{
	public static class DistributedCacheConfigurationExtensions
	{
		public static IDistributedCacheConfiguration AddCache<TModel, TDistributedCache>(
			this IDistributedCacheConfiguration caching,
			string prefix)
			where TDistributedCache : DistributedCache<TModel>
		{
			return caching.AddCache<string, TModel, TDistributedCache>(prefix);
		}
	}
}
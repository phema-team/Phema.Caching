using System;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	internal static class DistributedCacheHelper
	{
		public static string GetFullKey<TKey, TValue>(TKey key, PhemaDistributedCacheOptions options)
		{
			return $"{options.Prefixes[(typeof(TKey), typeof(TValue))]}{options.Separator}{key}";
		}
		
		public static DistributedCacheEntryOptions GetOptions<TKey, TValue>(PhemaDistributedCacheOptions options)
		{
			return options.Options[(typeof(TKey), typeof(TValue))];
		}
	}
}
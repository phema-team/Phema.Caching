using System;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	internal static class DistributedCacheHelper
	{
		public static string GetFullKey<TKey>(TKey key, Type type, DistributedCacheOptions options)
		{
			return $"{options.Prefixes[type]}{options.Separator}{key}";
		}
		
		public static DistributedCacheEntryOptions GetOptions<TValue>(DistributedCacheOptions options)
		{
			return options.Options[typeof(TValue)];
		}
	}
}
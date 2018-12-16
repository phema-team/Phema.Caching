using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

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
		
		public static TValue Deserialize<TValue>(byte[] data, DistributedCacheOptions options)
		{
			return JsonConvert.DeserializeObject<TValue>(options.Encoding.GetString(data), options.SerializerSettings);
		}

		public static byte[] Serialize<TValue>(TValue value, DistributedCacheOptions options)
		{
			return options.Encoding.GetBytes(JsonConvert.SerializeObject(value, options.SerializerSettings));
		}
	}
}
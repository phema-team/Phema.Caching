using System;
using Newtonsoft.Json;

namespace Phema.Caching
{
	internal static class DistributedCacheHelper
	{
		public static string GetFullKey<TKey>(TKey key, Type type, DistributedCacheOptions options)
		{
			return $"{options.Prefixes[type]}:{key}";
		}
		
		public static TValue Deserialize<TValue>(byte[] data, DistributedCacheOptions options)
		{
			return JsonConvert.DeserializeObject<TValue>(options.Encoding.GetString(data));
		}

		public static byte[] Serialize<TValue>(TValue value, DistributedCacheOptions options)
		{
			return options.Encoding.GetBytes(JsonConvert.SerializeObject(value));
		}
	}
}
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Caching
{
	internal static class DistributedCacheHelper
	{
		public static string GetFullKey<TKey, TValue>(TKey key, DistributedCacheOptions options)
		{
			var prefix = GetPrefix<TKey, TValue>();
			var cacheKey = new CacheKey<TKey>(prefix, key);

			return options.Serialize(cacheKey);
		}

		public static TValue Deserialize<TValue>(byte[] data, DistributedCacheOptions options)
		{
			return options.Deserialize<TValue>(options.Encoding.GetString(data));
		}

		public static byte[] Serialize<TValue>(TValue value, DistributedCacheOptions options)
		{
			return options.Encoding.GetBytes(options.Serialize(value));
		}

		[DataContract]
		private class CacheKey<TKey>
		{
			public CacheKey(string prefix, TKey key)
			{
				Prefix = prefix;
				Key = key;
			}

			[DataMember(Name = "prefix")]
			public string Prefix { get; }

			[DataMember(Name = "key")]
			public TKey Key { get; }
		}

		private static readonly IDictionary<(Type, Type), string> cache = new Dictionary<(Type, Type), string>();

		private static string GetPrefix<TKey, TValue>()
		{
			var key = typeof(TKey);
			var value = typeof(TValue);

			if (!cache.TryGetValue((key, value), out var prefix))
			{
				cache.Add((key, value), prefix = GeneratePrefix(key, value));
			}

			return prefix;
		}

		private static string GeneratePrefix(Type key, Type value)
		{
			var keyPart = key.GetCustomAttribute<DataContractAttribute>()?.Name ?? key.Name;
			var valuePart = value.GetCustomAttribute<DataContractAttribute>()?.Name ?? value.Name;

			return $"{keyPart}:{valuePart}";
		}
	}
}
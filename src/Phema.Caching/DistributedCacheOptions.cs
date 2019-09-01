using System;
using System.Text.Json;

namespace Phema.Caching
{
	public class DistributedCacheOptions
	{
		public DistributedCacheOptions()
		{
			Serializer = payload => JsonSerializer.SerializeToUtf8Bytes(payload);
			Deserializer = (bytes, type) => JsonSerializer.Deserialize(bytes, type);
		}

		internal Func<object, byte[]> Serializer { get; set; }
		internal Func<byte[], Type, object> Deserializer { get; set; }
	}
}
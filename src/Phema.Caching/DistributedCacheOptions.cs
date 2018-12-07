using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Phema.Caching
{
	public class DistributedCacheOptions
	{
		public DistributedCacheOptions()
		{
			Encoding = Encoding.UTF8;
			SerializerSettings = new JsonSerializerSettings();
			Prefixes = new Dictionary<Type, string>();
		}
		
		public Encoding Encoding { get; set; }

		public JsonSerializerSettings SerializerSettings { get; set; }
		
		internal IDictionary<Type, string> Prefixes { get; }
	}
}
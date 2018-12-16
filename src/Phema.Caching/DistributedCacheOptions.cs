using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
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
			Options = new Dictionary<Type, DistributedCacheEntryOptions>();
			Separator = ":";
		}

		public string Separator { get; set; }
		public Encoding Encoding { get; set; }

		public JsonSerializerSettings SerializerSettings { get; set; }
		
		internal IDictionary<Type, string> Prefixes { get; }
		internal Dictionary<Type, DistributedCacheEntryOptions> Options { get; set; }
	}
}
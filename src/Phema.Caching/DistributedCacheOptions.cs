using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	public class DistributedCacheOptions
	{
		public DistributedCacheOptions()
		{
			Prefixes = new Dictionary<Type, string>();
			Options = new Dictionary<Type, DistributedCacheEntryOptions>();
			Separator = ":";
		}

		public string Separator { get; set; }
		internal IDictionary<Type, string> Prefixes { get; }
		internal Dictionary<Type, DistributedCacheEntryOptions> Options { get; set; }
	}
}
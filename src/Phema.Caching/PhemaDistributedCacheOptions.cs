using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;

namespace Phema.Caching
{
	public sealed class PhemaDistributedCacheOptions
	{
		public PhemaDistributedCacheOptions()
		{
			Prefixes = new Dictionary<(Type, Type), string>();
			Options = new Dictionary<(Type, Type), DistributedCacheEntryOptions>();
			Separator = ":";
		}

		public string Separator { get; set; }
		internal IDictionary<(Type, Type), string> Prefixes { get; }
		internal Dictionary<(Type, Type), DistributedCacheEntryOptions> Options { get; }
	}
}
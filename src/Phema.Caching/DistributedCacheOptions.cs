using System;
using System.Collections.Generic;
using System.Text;

namespace Phema.Caching
{
	public class DistributedCacheOptions
	{
		public DistributedCacheOptions()
		{
			Encoding = Encoding.UTF8;
			Prefixes = new Dictionary<Type, string>();
		}

		public Encoding Encoding { get; set; }
		
		internal IDictionary<Type, string> Prefixes { get; }
	}
}
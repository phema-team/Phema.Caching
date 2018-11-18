using System.Runtime.Serialization;
using Phema.Configuration;

namespace Phema.Caching
{
	[DataContract]
	[Configuration]
	public class RedisConfiguration
	{
		[DataMember(Name = "name")]
		public string Name { get; private set; }

		[DataMember(Name = "host")]
		public string Host { get; private set; }

		[DataMember(Name = "port")]
		public int Port { get; private set; }

		[DataMember(Name = "password")]
		public string Password { get; private set; }

		[IgnoreDataMember]
		public string Configuration => $"{Host}:{Port},password={Password}"; 
	}
}
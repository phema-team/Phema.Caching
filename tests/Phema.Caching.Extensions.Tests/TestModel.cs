using System.Runtime.Serialization;

namespace Phema.Caching.Tests
{
	[DataContract(Name = "model")]
	public class TestModel
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
using System.Runtime.Serialization;

namespace Phema.Caching.Sandbox
{
	[DataContract(Name = "model")]
	public class TestModel
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
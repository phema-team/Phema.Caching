using System.Text;
using Newtonsoft.Json;

namespace Phema.Caching
{
	public class DistributedCacheJsonOptions : DistributedCacheOptions
	{
		public DistributedCacheJsonOptions()
		{
			Encoding = Encoding.UTF8;
			SerializerSettings = new JsonSerializerSettings();
		}
		
		public JsonSerializerSettings SerializerSettings { get; set; }
		
		protected override string Serialize<TValue>(TValue value)
		{
			return JsonConvert.SerializeObject(value, SerializerSettings);
		}

		protected override TValue Deserialize<TValue>(string data)
		{
			return JsonConvert.DeserializeObject<TValue>(data, SerializerSettings);
		}
	}
}
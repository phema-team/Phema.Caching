using System.Text;

namespace Phema.Caching
{
	public abstract class DistributedCacheOptions
	{
		protected DistributedCacheOptions()
		{
			Encoding = Encoding.UTF8;
		}
		
		public Encoding Encoding { get; set; }

		protected internal abstract string Serialize<TValue>(TValue value);

		protected internal abstract TValue Deserialize<TValue>(string data);
	}
}
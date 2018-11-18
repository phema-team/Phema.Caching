namespace Phema.Caching.Sandbox
{
	public class StringGenerator
	{
		public int Id { get; private set; }

		public string GetString()
		{
			return Id++.ToString();
		}
	}
}
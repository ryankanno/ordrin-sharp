namespace OrdrIn.Api.Resources
{
	public class Cuisine : ResourceBase
	{
		public Cuisine (string name)
		{
			this.Name = name;
		}

		public string Name { get; set; }
	}
}

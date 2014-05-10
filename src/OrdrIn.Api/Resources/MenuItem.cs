using System;

namespace OrdrIn.Api.Resources
{
	public class MenuItem : ResourceBase
	{
		public string UniqueId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public long PriceInCents { get; set; }
	}
}


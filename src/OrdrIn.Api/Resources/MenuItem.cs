namespace OrdrIn.Api.Resources
{
	using System;
	using System.Collections.Generic;

	public class MenuItem : ResourceBase
	{
		public string UniqueId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public long PriceInCents { get; set; }
		public ICollection<Availability> Availabilities { get; set; }
		public ICollection<OptionGroup> OptionGroups { get; set; }
	}
} 
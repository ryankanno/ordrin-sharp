namespace OrdrIn.Api
{
	using System;
	using System.Collections.Generic;
	using OrdrIn.Api.Resources;

	public class MenuGroup : ResourceBase
	{
		public string UniqueId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<MenuItem> MenuItems { get; set; }
	}
}


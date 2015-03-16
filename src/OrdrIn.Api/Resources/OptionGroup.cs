namespace OrdrIn.Api
{
	using System;
	using System.Collections.Generic;
	using OrdrIn.Api.Resources;

	public class OptionGroup : ResourceBase
	{
		public string UniqueId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int MinimumNumOptions { get; set; }
		public int MaximumNumOptions { get; set; }
		public ICollection<OptionItem> OptionItems { get; set; }
	}
}


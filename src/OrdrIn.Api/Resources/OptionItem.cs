using System.Collections.Generic;

namespace OrdrIn.Api
{
	using System;
	using OrdrIn.Api.Resources;

	public class OptionItem : ResourceBase
	{
		public string UniqueId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public long PriceInCents { get; set; }
	} 
}

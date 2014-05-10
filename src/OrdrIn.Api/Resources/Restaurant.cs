using System;
using System.Collections.Generic;

namespace OrdrIn.Api.Resources
{
	public enum Cuisine {
		
	}

	public class Restaurant : ResourceBase
	{
		public string Address { get; set; }
		public string City { get; set; }
		public ICollection<Cuisine> Cuisines { get; set; }
	}
}
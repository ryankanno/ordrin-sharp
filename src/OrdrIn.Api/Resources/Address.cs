using System;

namespace OrdrIn.Api.Resources
{
	public class Address : ResourceBase
	{
		public string AddressLine1 { get; set; }
		public string City { get; set; }
		public float Latitude { get; set; }
		public float Longitude { get; set; }
	}
}


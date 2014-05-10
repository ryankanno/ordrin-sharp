namespace OrdrIn.Api.Resources
{
	using System;
	using RestSharp.Deserializers;

	public class Address : ResourceBase
	{
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
		public string State { get; set; }
		public float Latitude { get; set; }
		public float Longitude { get; set; }
	}
}


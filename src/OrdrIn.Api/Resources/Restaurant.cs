namespace OrdrIn.Api.Resources
{
	using System;
	using System.Collections.Generic;
	using RestSharp.Deserializers;

	public class Restaurant : RestaurantBase
	{
		public ICollection<Cuisine> Cuisines { get; set; }
		public Menu Menu { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
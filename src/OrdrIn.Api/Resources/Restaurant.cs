namespace OrdrIn.Api.Resources
{
	using System;
	using System.Collections.Generic;
	using RestSharp.Deserializers;

	public class Restaurant : RestaurantBase
	{
		public ICollection<Cuisine> Cuisines { get; set; }
	}
}
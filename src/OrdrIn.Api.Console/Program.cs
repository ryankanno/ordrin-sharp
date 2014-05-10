namespace OrdrIn.Api.Console
{
	using System;
	using System.Configuration;

	class MainClass
	{
		public static void Main (string[] args)
		{
			var client = new OrdrInClient(ConfigurationManager.AppSettings["OrdrIn.ApiKey"]);
			var restaurant = client.GetRestaurantDetails ("147");
			System.Console.Write (restaurant.Name);
			var deliverableRestaurants = client.GetDeliveryList("ASAP", "10020", "New York", "520 Madison Avenue");
			System.Console.Write (deliverableRestaurants.Count);
		}
	}
}

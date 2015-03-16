using RestSharp.Serializers;
using OrdrIn.Api.Resources;

namespace OrdrIn.Api.Console
{
	using System;
	using System.Configuration;

	class MainClass
	{
		public static void Main (string[] args)
		{
			/**var restaurant = client.GetRestaurantDetails ("147");
			var serializer = new JsonSerializer();
			System.Console.Write (serializer.Serialize (restaurant));
			var deliverableRestaurants = client.GetDeliveryList("ASAP", "10020", "New York", "520 Madison Avenue");
			System.Console.Write (deliverableRestaurants.Count);
			var user = new User("ryankanno@gmail.com", "test", "Ryan", "Test");
			var results = client.CreateUser (user);
			**/
			var client = new OrdrInClient(ConfigurationManager.AppSettings["OrdrIn.ApiKey"]);
			var results = client.GetAccountInformation ("test10378@gmail.com", "10378");
		}
	}
}

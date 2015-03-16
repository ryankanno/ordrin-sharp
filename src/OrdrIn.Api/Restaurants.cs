namespace OrdrIn.Api
{
	using System.Collections.Generic;
	using OrdrIn.Api.Deserializers;
	using OrdrIn.Api.Resources;
	using RestSharp;
	using RestSharp.Deserializers;

	public partial class OrdrInClient
	{
#if DEBUG		
		private const string RestaurantApiBaseUrl = "https://r-test.ordr.in/";
#else
		private const string RestaurantApiBaseUrl = "https://r.ordr.in/";
#endif

		public virtual List<DeliverableRestaurant> GetDeliveryList(string datetime, string zipCode, string city, string address)
		{
			this.Client.BaseUrl = RestaurantApiBaseUrl;	
			this.Client.AddHandler("application/json", new JsonDeserializer());

			var request = new RestRequest();
			request.Resource = "dl/{DateTime}/{Zip}/{City}/{Address}";
			request.AddUrlSegment ("DateTime", datetime);
			request.AddUrlSegment ("Zip", zipCode);
			request.AddUrlSegment ("City", city);
			request.AddUrlSegment ("Address", address);

			return this.Execute<List<DeliverableRestaurant>> (request);
		}

		public virtual Restaurant GetRestaurantDetails(string restaurantId)
		{
			this.Client.BaseUrl = RestaurantApiBaseUrl;	
			this.Client.AddHandler("application/json", new RestaurantDeserializer());

			var request = new RestRequest();
			request.Resource = "rd/{RestaurantId}";
			request.AddUrlSegment ("RestaurantId", restaurantId);

			return this.Execute<Restaurant>(request);
		}
	}
} 
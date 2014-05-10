
namespace OrdrIn.Api
{
	using System.Collections.Generic;
	using OrdrIn.Api.Deserializers;
	using OrdrIn.Api.Resources;
	using RestSharp;

	public partial class OrdrInClient
	{
		public virtual List<DeliverableRestaurant> GetDeliveryList(string datetime, string zipCode, string city, string address)
		{
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
			this.AddRestaurantDeserializer ();
			var request = new RestRequest();
			request.Resource = "rd/{RestaurantId}";
			request.AddUrlSegment ("RestaurantId", restaurantId);
			var restaurant = this.Execute<Restaurant>(request);
			return restaurant;
		}

		/// <summary>
		/// Adds the restaurant deserializer. Haven't decided if I really need this yet.  Need to understand nested child mapping.
		/// </summary>
		private void AddRestaurantDeserializer()
		{
			this.client.AddHandler("application/json", new RestaurantDeserializer());
		}
	}
} 
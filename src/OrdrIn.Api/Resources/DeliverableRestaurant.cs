namespace OrdrIn.Api.Resources
{
	using RestSharp.Deserializers;

	public class DeliverableRestaurant : RestaurantBase
	{
		public string EstimatedDeliveryTime { get; set; }
		public string MinimumDelivery { get; set; }
	}
}

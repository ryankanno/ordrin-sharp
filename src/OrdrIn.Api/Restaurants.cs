namespace OrdrIn.Api
{
	using OrdrIn.Api.Resources;
	using RestSharp;

	public partial class OrdrInClient
	{
		public virtual Restaurant GetRestaurant(string restaurantId)
		{
			var request = new RestRequest();
			request.Resource = "rd/{RestaurantId}";
			request.AddParameter("RestaurantId", restaurantId, ParameterType.UrlSegment);
			return this.Execute<Restaurant>(request);
		}
	}
} 
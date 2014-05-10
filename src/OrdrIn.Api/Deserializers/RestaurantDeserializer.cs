namespace OrdrIn.Api.Deserializers
{
	using System;
	using System.Collections.Generic;
	using OrdrIn.Api.Resources;
	using OrdrIn.Api.Utilities;
	using RestSharp;
	using RestSharp.Deserializers;
	using RestSharp.Extensions;

	/// <summary>
	/// Controlling deserialization for now.  Might use generic funcs later.
	/// </summary>
	public class RestaurantDeserializer : OrdrInDeserializer, IDeserializer
	{
		#region IDeserializer implementation
		public override T Deserialize<T> (RestSharp.IRestResponse response) 
		{
			var jsonAsDictionary = SimpleJson.DeserializeObject(response.Content) as IDictionary<string, object>;
			var restaurant = Activator.CreateInstance<T>();
			this.MapOnlyRestaurant(restaurant, jsonAsDictionary);
			this.MapAddress(restaurant, jsonAsDictionary);
			return restaurant;
		}

		public override string RootElement { get; set; }
		public override string Namespace { get; set; }
		public override string DateFormat { get; set; }
		#endregion

		protected void MapOnlyRestaurant<T>(T instance, IDictionary<string, object> jsonDictionary)
		{
			this.SetProperty (instance, "UniqueId", jsonDictionary.GetValueOrDefault ("restaurant_id", (object value) => {return value as string; }));
			this.SetProperty (instance, "Name", jsonDictionary.GetValueOrDefault ("name", (object value) => {return value as string; }));
			this.SetProperty (instance, "PhoneNumber", jsonDictionary.GetValueOrDefault ("cs_contact_phone", (object value) => {return value as string; }));
			this.MapCuisine(instance, jsonDictionary);
		}

		protected void MapCuisine<T>(T instance, IDictionary<string, object> jsonDictionary)
		{
			var cuisines = jsonDictionary.GetValueOrDefault<string, object, IList<object>>("cuisine", (object value) => { return value as IList<object>; });
			var cuisineList = new List<Cuisine>();
			foreach (string cuisine in cuisines)
			{
				cuisineList.Add (new Cuisine(cuisine));	
			}
			var prop = typeof(T).GetProperty ("Cuisines");
			prop.SetValue (instance, cuisineList, null);
		}

		protected void MapAddress<T>(T instance, IDictionary<string, object> jsonDictionary)
		{
			Address address = new Address();
			address.AddressLine1 = jsonDictionary.GetValueOrDefault<string, object, string>("addr", (object value) => { return value as string; });
			address.City = jsonDictionary.GetValueOrDefault<string, object, string>("city", (object value) => { return value as string; });
			address.ZipCode = jsonDictionary.GetValueOrDefault<string, object, string> ("postal_code", (object value) => { return value as string; });
			address.Latitude = jsonDictionary.GetValueOrDefault<string, object, float> ("latitude", ParseFloat);
			address.Longitude = jsonDictionary.GetValueOrDefault<string, object, float> ("longitude", ParseFloat);
			var prop = typeof(T).GetProperty ("Address");
			prop.SetValue (instance, address, null);
		}

		protected void MapMenu<T>(T instance, IDictionary<string, object> jsonDictionary)
		{
			Menu menu = new Menu();
			var prop = typeof(T).GetProperty ("Menu");
			prop.SetValue (instance, menu, null);
		}

		private float ParseFloat(object value)
		{
			try 
			{
				return float.Parse(value as string, System.Globalization.CultureInfo.InvariantCulture);
			}
			catch
			{
				return float.MinValue;
			}
		}

		private void SetProperty<T>(T instance, string propertyName, object value)
		{
			var prop = typeof(T).GetProperty (propertyName);
			if (null != prop)
			{
				prop.SetValue(instance, value, null);
			}
		}
	}
}
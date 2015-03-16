namespace OrdrIn.Api.Deserializers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
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
			this.MapRestaurant(restaurant, jsonAsDictionary);
			return restaurant;
		}

		public override string RootElement { get; set; }
		public override string Namespace { get; set; }
		public override string DateFormat { get; set; }
		#endregion

		protected virtual void MapRestaurant<T>(T instance, IDictionary<string, object> jsonDictionary)
		{
			this.SetProperty (instance, "UniqueId", jsonDictionary.GetValueOrDefault ("restaurant_id", (object value) => {return value as string; }));
			this.SetProperty (instance, "Name", jsonDictionary.GetValueOrDefault ("name", (object value) => {return value as string; }));
			this.SetProperty (instance, "PhoneNumber", jsonDictionary.GetValueOrDefault ("cs_contact_phone", (object value) => {return value as string; }));
			this.SetProperty (instance, "Latitude", jsonDictionary.GetValueOrDefault ("latitude", ParseDouble));
			this.SetProperty (instance, "Longitude", jsonDictionary.GetValueOrDefault ("longitude", ParseDouble));
			this.SetProperty (instance, "Cuisine", this.MapCuisine(jsonDictionary));
			this.SetProperty (instance, "Address", this.MapAddress (jsonDictionary));
			this.SetProperty (instance, "Menu", this.MapMenu (jsonDictionary));
		}

		protected virtual ICollection<Cuisine> MapCuisine(IDictionary<string, object> jsonDictionary)
		{
			var cuisines = jsonDictionary.GetValueOrDefault<string, object, IList<object>>("cuisine", (object value) => { return value as IList<object>; });
			return cuisines.Select(cuisine => new Cuisine(cuisine as string)).ToList();
		}

		protected virtual Address MapAddress(IDictionary<string, object> jsonDictionary)
		{
			var address = new Address();
			address.AddressLine1 = jsonDictionary.GetValueOrDefault<string, object, string>("addr", (object value) => { return value as string; });
			address.City = jsonDictionary.GetValueOrDefault<string, object, string>("city", (object value) => { return value as string; });
			address.ZipCode = jsonDictionary.GetValueOrDefault<string, object, string> ("postal_code", (object value) => { return value as string; });
			return address;
		}

		protected virtual Menu MapMenu(IDictionary<string, object> jsonDictionary)
		{
			var menu = new Menu();
			menu.MenuGroups = this.MapJsonListToType(jsonDictionary["menu"] as IList<object>, (IDictionary<string, object> item) => this.MapMenuGroup(item));
			return menu;
		}

		protected virtual MenuGroup MapMenuGroup(IDictionary<string, object> jsonMenuGroup)
		{
			var menuGroup = new MenuGroup();
			menuGroup.UniqueId = jsonMenuGroup.GetValueOrDefault<string, object, string> ("id", (object value) => { return value as string; });
			menuGroup.Name = jsonMenuGroup.GetValueOrDefault<string, object, string> ("name", (object value) => { return value as string; });
			menuGroup.Description = jsonMenuGroup.GetValueOrDefault<string, object, string> ("descrip", (object value) => { return value as string; });
			if (jsonMenuGroup.ContainsKey ("children"))
			{	
				menuGroup.MenuItems = this.MapJsonListToType(jsonMenuGroup["children"] as IList<object>, (IDictionary<string, object> item) => this.MapMenuItem(item));
			}
			return menuGroup;
		}

		// TODO: Refactor MenuItem to OptionItem
		protected virtual MenuItem MapMenuItem(IDictionary<string, object> jsonMenuItem)
		{
			var menuItem = new MenuItem();
			menuItem.UniqueId = jsonMenuItem.GetValueOrDefault<string, object, string> ("id", (object value) => { return value as string; });
			menuItem.Name = jsonMenuItem.GetValueOrDefault<string, object, string> ("name", (object value) => { return value as string; });
			menuItem.Description = jsonMenuItem.GetValueOrDefault<string, object, string> ("descrip", (object value) => { return value as string; });
			menuItem.PriceInCents = (long)(jsonMenuItem.GetValueOrDefault<string, object, decimal> ("price", ParseMoney) * 100);
			if (jsonMenuItem.ContainsKey ("children"))
			{
				menuItem.OptionGroups = this.MapJsonListToType(jsonMenuItem["children"] as IList<object>, (IDictionary<string, object> item) => this.MapOptionGroup(item));
			}
			return menuItem;
		}

		protected virtual OptionGroup MapOptionGroup(IDictionary<string, object> jsonOptionGroup)
		{
			var optionGroup = new OptionGroup ();
			optionGroup.UniqueId = jsonOptionGroup.GetValueOrDefault<string, object, string> ("id", (object value) => { return value as string; });
			optionGroup.Name = jsonOptionGroup.GetValueOrDefault<string, object, string> ("name", (object value) => { return value as string; });
			optionGroup.Description = jsonOptionGroup.GetValueOrDefault<string, object, string> ("descrip", (object value) => { return value as string; });
			optionGroup.MinimumNumOptions = jsonOptionGroup.GetValueOrDefault<string, object, int> ("min_child_select", (object value) => { return Convert.ToInt32(value as string); });
			optionGroup.MaximumNumOptions = jsonOptionGroup.GetValueOrDefault<string, object, int> ("max_child_select", (object value) => { return Convert.ToInt32(value as string); });
			if (jsonOptionGroup.ContainsKey ("children"))
			{	
				optionGroup.OptionItems= this.MapJsonListToType(jsonOptionGroup["children"] as IList<object>, (IDictionary<string, object> item) => this.MapOptionItem(item));
			}
			return optionGroup;
		}

		protected virtual OptionItem MapOptionItem(IDictionary<string, object> jsonOptionItem)
		{
			var optionItem = new OptionItem();
			optionItem.UniqueId = jsonOptionItem.GetValueOrDefault<string, object, string> ("id", (object value) => { return value as string; });
			optionItem.Name = jsonOptionItem.GetValueOrDefault<string, object, string> ("name", (object value) => { return value as string; });
			optionItem.Description = jsonOptionItem.GetValueOrDefault<string, object, string> ("descrip", (object value) => { return value as string; });
			optionItem.PriceInCents = (long)(jsonOptionItem.GetValueOrDefault<string, object, decimal> ("price", ParseMoney) * 100);
			return optionItem;
		}
	}
}
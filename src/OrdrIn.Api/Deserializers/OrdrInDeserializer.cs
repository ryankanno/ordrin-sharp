namespace OrdrIn.Api.Deserializers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using RestSharp;
	using RestSharp.Deserializers;
	using RestSharp.Extensions;

	public abstract class OrdrInDeserializer : IDeserializer
	{
		/// <summary>
		/// Finds the root element. Taken from https://github.com/restsharp/RestSharp/blob/master/RestSharp/Deserializers/JsonDeserializer.cs
		/// </summary>
		protected virtual object GetRoot(string content)
		{
			var data = (IDictionary<string, object>)SimpleJson.DeserializeObject(content);
			if (this.RootElement.HasValue() && data.ContainsKey(this.RootElement))
			{
				return data[RootElement];
			}
			return data;
		}

		#region IDeserializer implementation
		public abstract T Deserialize<T> (IRestResponse response);
		public abstract string RootElement { get; set; }
		public abstract string Namespace { get; set; }
		public abstract string DateFormat { get; set; }
		#endregion

		protected double ParseDouble(object value)
		{
			return this.Parse<double>(
				((v) => double.Parse(v as string, System.Globalization.CultureInfo.InvariantCulture)), 
				value, 
				double.MinValue);
		}

		protected decimal ParseMoney(object value)
		{
			return this.Parse<decimal>(
				((v) => decimal.Parse(v as string, System.Globalization.NumberStyles.Currency)),
				value, 
				decimal.MinValue);
		}

		protected virtual T Parse<T>(Func<object, T> parseFunc, object value, T defaultValue)
		{
			try
			{
				return parseFunc(value); 
			}
			catch
			{
				return defaultValue;
			}
		}

		protected ICollection<T> MapJsonListToType<T, U>(IList<object> jsonItems, Func<U, T> func) where U : class
		{
			return jsonItems.Select(item => func(item as U)).ToList();
		}

		protected void SetProperty<T>(T instance, string propertyName, object value)
		{
			var prop = typeof(T).GetProperty (propertyName);
			if (null != prop)
			{
				prop.SetValue(instance, value, null);
			}
		}
	}
} 
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
	}
}


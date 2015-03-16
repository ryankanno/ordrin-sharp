namespace OrdrIn.Api
{
	using System;
	using RestSharp.Deserializers;

 	public class DynamicJsonDeserializer : IDeserializer
	{
		#region IDeserializer implementation
		public T Deserialize<T> (RestSharp.IRestResponse response)
		{
			var serializer = new JsonDeserializer ();
			return serializer.Deserialize<dynamic> (response);
		}

		public string RootElement { get; set; }
		public string Namespace { get; set; }
		public string DateFormat { get; set; }
		#endregion
    }    
} 
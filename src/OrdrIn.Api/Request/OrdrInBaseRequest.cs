namespace OrdrIn.Api
{
	using System;
	using RestSharp;

	public class OrdrInBaseRequest : RestRequest
	{
		public OrdrInBaseRequest (string resource, Method method) : base(resource, method)
		{
			this.RequestFormat = RestSharp.DataFormat.Json;
		}
	}
} 
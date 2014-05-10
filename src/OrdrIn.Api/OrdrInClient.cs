using System;
using RestSharp;
using System.Net;

namespace OrdrIn.Api
{
	public partial class OrdrInClient
	{
		private readonly string accountPublicKey;
		private RestClient client;
		
		public OrdrInClient (string accountPublicKey, string accountPrivateKey)
		{
			this.ApiVersion = "1";
			this.ApiBaseUrl = "http://foo.bar";
			this.accountPublicKey = accountPublicKey;
			this.client = this.GetRestClientImpl();
		}

		public string ApiVersion { get; private set; }
		public string ApiBaseUrl { get; private set; }

		public IWebProxy Proxy 
		{ 
			get 
			{
				return this.client.Proxy;
			}	
			set
			{
				this.client.Proxy = value;	
			}
		}	

		protected virtual T Execute<T>(RestRequest request) 
			where T : new()
		{
			var response = this.client.Execute<T>(request);
			return response.Data;
		}

		private RestClient GetRestClientImpl() 
		{
			return new RestClient();	
		}

		private void AddClientAuthenticationHeader(RestClient client)
		{
			var authenticationHeader = this.GetAuthenticationHeader (this.accountPublicKey, this.ApiVersion);
			client.AddDefaultHeader ("X-NAAMA-CLIENT-AUTHENTICATION", authenticationHeader);
		}

		private string GetAuthenticationHeader(string authenticationKey, string version)
		{
			return string.Format("id=\"{0}\", version=\"{1}\"", authenticationKey, version);
		}
	}
} 
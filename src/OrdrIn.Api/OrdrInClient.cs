using System;
using RestSharp;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace OrdrIn.Api
{
	public partial class OrdrInClient
	{
		private const string AuthenticationHeader = "X-NAAMA-CLIENT-AUTHENTICATION";
		private const string AuthenticationHeaderFormat = "id=\"{0}\", version=\"{1}\"";

		private readonly string accountPrivateKey;
		private readonly Lazy<RestClient> client;
		
		public OrdrInClient (string accountPrivateKey) 
		{
			this.accountPrivateKey = accountPrivateKey;
			this.client = new Lazy<RestClient>(() => this.GetRestClientImpl(this.accountPrivateKey, this.ApiVersion)); 
		}

		public string ApiVersion { get { return "1"; } }
		public string ApiBaseUrl { get; set; } 

		public IWebProxy Proxy 
		{ 
			get 
			{
				return this.Client.Proxy;
			}	
			set
			{
				this.Client.Proxy = value;	
			}
		}	

		protected RestClient Client 
		{
			get
			{
				return this.client.Value;		
			}
		}

		protected virtual T Execute<T>(IRestRequest request) 
			where T : new()
		{
			var response = this.Client.Execute<T>(request);
			return response.Data;
		}

		protected virtual IRestResponse Execute(IRestRequest request)
		{
			return this.Client.Execute(request);
		}

		private RestClient GetRestClientImpl(string privateKey, string version)
		{
			var client = new RestClient ();
			client.AddDefaultHeader (AuthenticationHeader, string.Format(AuthenticationHeaderFormat, privateKey, version));
			return client;
		}

		private void SetClientBaseUrl(string baseUrl)
		{
			this.Client.BaseUrl = baseUrl;		
		}
    }
} 
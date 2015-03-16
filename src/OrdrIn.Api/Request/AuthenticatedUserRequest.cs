using RestSharp.Contrib;

namespace OrdrIn.Api
{
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using RestSharp;

	public class AuthenticatedUserRequest : OrdrInBaseRequest
	{
		private const string UserAuthenticationHeader = "X-NAAMA-AUTHENTICATION";
		private const string UserAuthenticationHeaderFormat = "username=\"{0}\", response=\"{1}\", version=\"{2}\"";

		public AuthenticatedUserRequest (string resource, Method method, string version, string email, string password) 
			: base(resource, method)
		{
			this.AddHeader(UserAuthenticationHeader, this.GetAuthenticationHeaderWithUser (version, email, password, resource));
		}

		private string GetAuthenticationHeaderWithUser(string version, string email, string password, string resource)
		{
			var encodedResource = HttpUtility.UrlEncode(resource);
			var response = CryptoUtilities.GetSHA256(string.Format("{0}{1}{2}", password, email, encodedResource)); 
			return string.Format(UserAuthenticationHeaderFormat, email, response, version);
		}
	}
}


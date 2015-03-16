namespace OrdrIn.Api
{
	using System;
	using OrdrIn.Api.Resources;
	using RestSharp;
	using RestSharp.Contrib;
	using RestSharp.Deserializers;
	using RestSharp.Validation;

	public partial class OrdrInClient
	{
#if DEBUG		
		private const string UserApiBaseUrl = "https://u-test.ordr.in/";
#else
		private const string UserApiBaseUrl = "https://u.ordr.in/";
#endif

		public dynamic CreateUser(User user)
		{
			Require.Argument ("user", user);

			this.SetClientBaseUrl (UserApiBaseUrl);
			this.Client.AddHandler("application/json", new DynamicJsonDeserializer());
			var uri = string.Format("/u/{0}", user.EmailAddress);
			var request = new OrdrInBaseRequest(uri, Method.POST);
			request.AddParameter("email", user.EmailAddress);
			request.AddParameter("password", user.Password);
			request.AddParameter("first_name", user.FirstName);
			request.AddParameter("first_name", user.LastName);
			return this.Client.Execute<dynamic>(request);
		}

		public dynamic GetAccountInformation(string email, string password)
		{
			Require.Argument ("email", email);
			Require.Argument ("password", password);

			this.SetClientBaseUrl (UserApiBaseUrl);
			var shaPassword = CryptoUtilities.GetSHA256 (password);
			var request = new AuthenticatedUserRequest (string.Format("/u/{0}", email), Method.GET, this.ApiVersion, email, shaPassword);
			return this.Client.Execute<dynamic>(request);
		}
	}
}


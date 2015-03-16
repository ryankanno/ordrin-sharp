using System;

namespace OrdrIn.Api.Resources
{
	public class User : ResourceBase
	{
		private string password; 

		public User(string emailAddress, string password, string firstName = "", string lastName = "")
		{
			this.EmailAddress = emailAddress;
			this.Password = password;
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		public string UniqueId { get; set; }
		public string EmailAddress { get; set; }
		public string Password 
		{ 
			get 
			{
				return CryptoUtilities.GetSHA256 (this.password);
			}
			set 
			{
				this.password = value;
			}
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}


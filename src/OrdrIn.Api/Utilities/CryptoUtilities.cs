namespace OrdrIn.Api
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	public static class CryptoUtilities
	{
		public static string GetSHA256(string stringToHash)
		{
        	using (var crypt = new SHA256Managed())
			{
		        var hash = String.Empty;
				var bytes = Encoding.UTF8.GetBytes(stringToHash);
				var crypto = crypt.ComputeHash(bytes, 0, bytes.Length);
		        foreach (var bit in crypto)
				{
		            hash += bit.ToString("x2");
				}
				return hash;
			}
		}
	}
} 
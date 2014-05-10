namespace OrdrIn.Api.Utilities
{
	using System;
	using System.Collections.Generic;

	public static class DictionaryExtensions
	{
		public static TConvert GetValueOrDefault<TKey, TValue, TConvert>(
			this IDictionary<TKey, TValue> dictionary, 
			TKey key, 
			Func<TValue, TConvert> conversionFunc)
		{
		    TValue ret;
    		dictionary.TryGetValue(key, out ret);
			return conversionFunc(ret);
		}
	}
}
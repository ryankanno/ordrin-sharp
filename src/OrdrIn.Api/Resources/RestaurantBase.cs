namespace OrdrIn.Api.Resources
{
	using System;

	public abstract class RestaurantBase : ResourceBase
	{
		public virtual string UniqueId { get; set; }
		public virtual string Name { get; set; }    
		public virtual string PhoneNumber { get; set; }    
		public virtual Address Address { get; set; }    
	}
}


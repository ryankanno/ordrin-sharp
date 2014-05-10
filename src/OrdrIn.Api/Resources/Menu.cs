using System;
using System.Collections.Generic;

namespace OrdrIn.Api.Resources
{
	public class Menu : ResourceBase
	{
		public Menu() 
		{
			this.MenuItems = new List<MenuItem>();
		}

		public ICollection<MenuItem> MenuItems { get; set; }
	}
}

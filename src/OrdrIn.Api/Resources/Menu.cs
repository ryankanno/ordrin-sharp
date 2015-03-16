using System;
using System.Collections.Generic;

namespace OrdrIn.Api.Resources
{
	public class Menu : ResourceBase
	{
		public Menu() 
		{
			this.MenuGroups = new List<MenuGroup>();
		}

		public ICollection<MenuGroup> MenuGroups { get; set; }
	}
}

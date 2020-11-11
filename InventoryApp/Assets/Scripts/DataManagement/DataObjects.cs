using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
	[System.Serializable]
	public class Product : JsonObject
	{
		public override string PreferredLocation
		{
			get
			{
				return "Product";
			}
		}
	}

}

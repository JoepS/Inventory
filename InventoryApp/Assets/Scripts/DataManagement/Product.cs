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

		//Data
		public int id;
		public string name;
		public string description;
		public bool perishable;
		public float cost;
		//public string buyLocation;

		public override string ToString()
		{
			return string.Format("{0} | {1} | {2} | {3} | {4}", id, name, description, perishable, cost);
		}

	}

}

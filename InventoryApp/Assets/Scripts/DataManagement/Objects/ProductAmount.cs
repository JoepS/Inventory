using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
	[System.Serializable]
	public class ProductAmount : JsonObject
	{
		public int ProductId;
		public Amount amount;
		public string perishDate;

		new public static string location = "/ProductAmounts/ProductAmount";


		public override string PreferredLocation
		{
			get
			{
				return location;
			}
		}

		public override string ToString()
		{
			return string.Format("{0} | {1} | {2}", ProductId, amount, perishDate);
		}

		//public override JsonObject Copy()
		//{
		//	return new ProductAmount()
		//	{
		//		ProductId = this.ProductId,
		//		amount = this.amount,
		//		perishDate = this.perishDate
		//	};
		//}
	}
}

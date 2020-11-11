using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
	[System.Serializable]
	public class ProductAmount
	{
		public int ProductId;
		public Amount amount;
		public System.DateTime perishDate;
	}
}

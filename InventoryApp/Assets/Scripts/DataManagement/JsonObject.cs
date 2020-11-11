using UnityEngine;

namespace DataManagement
{
	[System.Serializable]
	public abstract class JsonObject
	{
		public abstract string PreferredLocation
		{
			get;
		}
		public abstract string ToJson();
	}

}
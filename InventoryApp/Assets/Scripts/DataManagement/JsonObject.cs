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
		public virtual string ToJson()
		{
			return JsonUtility.ToJson(this);
		}
	}

}
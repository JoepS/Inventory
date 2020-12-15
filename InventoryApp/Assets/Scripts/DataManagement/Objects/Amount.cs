namespace DataManagement
{
	[System.Serializable]
	public class Amount : JsonObject
	{
		public float value;
		public Type type;

		public override string PreferredLocation
		{
			get
			{
				return "Amount/Amounts";
			}
		}

		public enum Type
		{
			Grams,
			Kilogram,
			Units,
			Single
		}

		public override string ToString()
		{
			return string.Format("Amount: {0} {1}", value, type);
		}
	}

	
}
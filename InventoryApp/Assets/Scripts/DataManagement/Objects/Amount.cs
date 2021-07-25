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

		//public Amount(Amount other) : base(other)
		//{
		//	this.value = other.value;
		//	this.type = other.type;
		//}

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
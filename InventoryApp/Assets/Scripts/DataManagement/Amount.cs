namespace DataManagement
{
	public class Amount
	{
		public float value;
		public Type type;
		public enum Type
		{
			Grams,
			Kilogram,
			Units,
			Single
		}
	}

	
}
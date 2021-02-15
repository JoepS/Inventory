using UnityEngine;
using SQLite4Unity3d;

namespace DataManagement.Models
{

    public class Product : Model
    {
        [NotNull]
        public string name { get; set; }

        [NotNull]
        public string description { get; set; }

        [NotNull]
        public float price { get; set; }

        [NotNull]
        public bool perishable { get; set; }

		public static new Product Where(int id)
		{
            try
            {
                return DatabaseController.connection.Table<Product>().Where(x => x.id == id).First();
            }
            catch (System.Exception ex)
			{
                Debug.LogError("Could not find Product with id : " + id + "\n" + ex.ToString());
                return default;
			}
		}

		public override string ToString()
		{
            return string.Format("{0}, {1}, {2}, {3}, {4}", id, name, description, price, perishable);
		}
	}

}

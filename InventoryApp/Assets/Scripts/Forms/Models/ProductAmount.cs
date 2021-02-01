using SQLite4Unity3d;
using UnityEngine;

namespace DataManagement.Models
{

    public class ProductAmount : Model
    {
        [NotNull]
        public int product_id { get; set; }

        [NotNull]
        public float amount { get; set; }

        [NotNull]
        public int amount_type_id { get; set; }

        [NotNull]
        public string expiration_date { get; set; }

        public static new ProductAmount Where(int id)
        {
            try
            {
                return DatabaseController.connection.Table<ProductAmount>().Where(x => x.id == id).First();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Could not find ProductAmount with id : " + id + "\n" + ex.ToString());
                return default;
            }
        }

		public override string ToString()
		{
            return string.Format("{0}, {1}, {2, {3}, {4}", id, product_id, amount, amount_type_id, expiration_date);
		}
	}

}

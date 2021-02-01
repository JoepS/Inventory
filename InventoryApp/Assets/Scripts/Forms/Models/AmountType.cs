using UnityEngine;
using SQLite4Unity3d;

namespace DataManagement.Models
{
    public class AmountType : Model
    {
        [NotNull]
        public string name { get; set; }

        public static new AmountType Where(int id)
        {
            try
            {
                return DatabaseController.connection.Table<AmountType>().Where(x => x.id == id).First();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Could not find AmountType with id : " + id + "\n" + ex.ToString());
                return default;
            }
        }
    }

}
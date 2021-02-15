using SQLite4Unity3d;

namespace DataManagement.Models
{

    public class Model
    {
        [PrimaryKey, NotNull, AutoIncrement, Unique]
        public int id { get; set; }

        public void Save()
        {
            DatabaseController.connection.Update(this);
        }

        public void New()
        {
            DatabaseController.connection.Insert(this);
        }

        public static Model Where(int id) { throw new System.NotImplementedException("Need to create new Where method"); }
    }

}

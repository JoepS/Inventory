using SQLite4Unity3d;
using System.IO;
using UnityEngine;
using DataManagement.Models;

namespace DataManagement
{
    public class DatabaseController
    {
        public static SQLiteConnection connection
        {
            get
            {
                return _connection;
            }
        }

        private static SQLiteConnection _connection;

        public static void Connect(string DatabaseName)
        {

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            try
            {
                AmountType type = _connection.Table<AmountType>().Where(x => x.id == 1).First();
            }
            catch (SQLiteException ex)
			{
                Debug.LogWarning("New database needs creation " + ex);
                CreateDatabaseTables();
                PopulateDefaultValues();
			}


            Debug.Log("Final PATH: " + dbPath);

        }

        private static void CreateDatabaseTables()
		{
            _connection.CreateTable<Product>();
            _connection.CreateTable<ProductAmount>();
            _connection.CreateTable<AmountType>();

            Debug.Log("Done creating database tables");
		}

        private static void PopulateDefaultValues()
		{
            _connection.InsertAll(new[]{
                new AmountType()
                {
                    name= "Unit"
                },
                new AmountType()
                {
                    name = "Grams"
                },
                new AmountType()
                {
                    name = "Kilograms"
                }
            });

            Debug.Log("Done populating default values");
        }

        public static void Disconnect()
		{
            _connection.Close();
		}
    }

}

using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace DataManagement
{

    public class JsonSerializer
    {

        public void SaveData(JsonObject jsonObject)
        {
            SaveData(jsonObject, jsonObject.PreferredLocation);
        }

        private void SaveData(JsonObject jsonObject, string location)
        {
            try
            {
                string json = jsonObject.ToJson();
                string path = Application.persistentDataPath + location;
                string directoryPath = path;
                int index = path.LastIndexOf("/");
                if (index > 0)
                     directoryPath = directoryPath.Substring(0, index);

                if (!Directory.Exists(directoryPath))
                {

                    Directory.CreateDirectory(directoryPath);
                }

                if (!path.EndsWith(".json"))
                    path += ".json";

                

                //var file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                var writer = new StreamWriter(path, false);
                writer.Write(json);
                writer.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning("File Save Exception: " + e.Message);
            }
        }

        public T LoadData<T>(string location) where T : JsonObject
        {
            try
            {
                string path = Application.persistentDataPath + location;
                if (!path.Contains(".json"))
                    path += ".json";
                
                var file = File.Open(path, FileMode.Open, FileAccess.Read);
                StreamReader stream = new StreamReader(file);
                string json = stream.ReadToEnd();
                stream.Close();

                if (!string.IsNullOrEmpty(json))
                {
                    T jsonObject = JsonUtility.FromJson<T>(json);
                    return jsonObject;
                }
                else
                {
                    Debug.LogWarning("File is empty: " + (Application.persistentDataPath + location));
                    return default(T);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("File Read Exception: " + e.Message);
                return default(T);
            }
        }
    }

}

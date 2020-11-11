using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DataManagement;

namespace Testing
{

    public class JsonTesting : MonoBehaviour
    {

        JsonSerializer serializer;

        // Start is called before the first frame update
        void Start()
        {
            serializer = new JsonSerializer();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Saving");
                serializer.SaveData(new TestObject());
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("Loading");
                TestObject testObject = serializer.LoadData<TestObject>(TestObject.location);

                if (testObject != null)
                    Debug.Log(testObject.test);
            }

        }
    }

    [System.Serializable]
    public class TestObject : JsonObject
    {
        public string test = "Hello World";

        public static string location = "/Test/Testing/TestObject";


        public override string PreferredLocation
        {
            get
            {
                return location;
            }
        }

        public override string ToJson()
        {
            string json = JsonUtility.ToJson(this);
            Debug.Log(json);
            return json;
        }
    }

}

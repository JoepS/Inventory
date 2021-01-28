using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SQLiteTesting : MonoBehaviour
{
    DatabaseController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = new DatabaseController("database.db");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnApplicationQuit()
	{
        controller.connection.Close();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagement;
using DataManagement.Models;
using System.Linq;

namespace Testing
{

    public class SQLiteTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DatabaseController.Connect("database.db");

            Debug.Log(Product.Where(1));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnApplicationQuit()
        {
            DatabaseController.Disconnect();
        }
    }

}

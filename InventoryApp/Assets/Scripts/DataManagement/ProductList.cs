using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
    [System.Serializable]
    public class ProductList : JsonObject
    {
        new public static string location = "/Products/List";

        public List<Product> products;

        public override string PreferredLocation
        {
            get
            {
                return location;
            }
        }

        public override string ToString()
        {
            string s = "";

            foreach(Product product in products)
            {
                s += product + "\n";
            }

            return s;
        }
    }

}

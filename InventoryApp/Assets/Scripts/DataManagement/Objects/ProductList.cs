using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DataManagement
{
    [System.Serializable]
    public class ProductList : JsonObjectList<Product>
    {
        new public static string location = "/Products/List";

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

            foreach(Product product in list)
            {
                s += product + "\n";
            }

            return s;
        }
    }

}

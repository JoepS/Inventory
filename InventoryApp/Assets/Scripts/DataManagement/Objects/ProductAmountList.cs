using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{

    public class ProductAmountList : JsonObjectList<ProductAmount>
    {
        new public static string location = "/Amounts/List";

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

            foreach (ProductAmount product in list)
            {
                s += product + "\n";
            }

            return s;
        }
    }

}

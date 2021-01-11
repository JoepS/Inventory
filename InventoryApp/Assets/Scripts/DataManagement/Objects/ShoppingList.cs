using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{

    public class ShoppingList : JsonObjectList<ProductAmount>
    {
        new public static string location = "/ShopingList/ShoppingList";

        public override string PreferredLocation
        {
            get
            {
                return location;
            }
        }
        public override string ToString()
        {
            string s = "ShopingList: \n";

            foreach (ProductAmount product in list)
            {
                s += product + "\n";
            }

            return s;
        }
    }

}

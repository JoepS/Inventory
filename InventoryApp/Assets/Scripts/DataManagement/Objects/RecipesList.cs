using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{

    public class RecipesList : JsonObjectList<Recipe>
    {
        new public static string location = "/Recipes/List";

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

            foreach (Recipe recipe in list)
            {
                s += recipe + "\n";
            }

            return s;
        }
    }

}

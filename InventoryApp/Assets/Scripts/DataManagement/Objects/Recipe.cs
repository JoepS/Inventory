using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagement
{
    [System.Serializable]
    public class Recipe : JsonObject
    {
        public override string PreferredLocation
        {
            get
            {
                return "Recipe";
            }
        }

        public string name;
        public string description;
        public float preperationTime;
        public List<ProductAmount> ingredients;


        public override string ToString()
        {
            string s = "\nIngredients:\n";
            foreach(ProductAmount pa in ingredients)
            {
                s += pa + "\n";
            }

            return string.Format("{0} | {1} | {2} | {3} | {4}", id, name, description, preperationTime, s);
        }

    }

}

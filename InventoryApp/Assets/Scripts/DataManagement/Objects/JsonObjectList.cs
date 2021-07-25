using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataManagement
{

    public class JsonObjectList<T> : JsonObject where T : JsonObject
    {
        public List<T> list = new List<T>();

        public int GetUniqueID()
        {
            if (list.Count > 0)
            {
                int id = list.OrderByDescending(x => x.id).First().id;
                if (id > 0)
                {
                    return id + 1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public override string PreferredLocation {
            get
            {
                return "";
            }
        }
    }

}

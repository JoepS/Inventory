using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{

    public class Framework : MonoBehaviour
    {
        public static Framework instance = null;

		private void Awake()
		{
			if(instance == null)
			{
				instance = this;
				DontDestroyOnLoad(this.gameObject);
			}
			else
			{
				DestroyImmediate(this.gameObject);
			}

			DataManagement.DatabaseController.Connect("database.db");
		}
	}

}

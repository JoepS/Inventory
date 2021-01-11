using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framework : MonoBehaviour
{
    public static Framework instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        Debug.Log("RuntimeVersion: " + typeof(string).Assembly.ImageRuntimeVersion);

    }
}

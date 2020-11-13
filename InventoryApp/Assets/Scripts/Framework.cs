using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framework : MonoBehaviour
{
    public static Framework instance = null;

    [SerializeField]
    private DataManagement.DataManager dataManager = null;

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

        if(dataManager == null)
        {
            dataManager = new GameObject("DataManager").AddComponent<DataManagement.DataManager>();
            Debug.Log(dataManager + " / " + this);
            dataManager.gameObject.transform.SetParent(this.transform);
        }

    }
}

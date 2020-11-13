using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiTabView : MonoBehaviour
{
    public static MultiTabView instance = null;

    private List<TabView> tabs = null;

    private int OpenedTab = -1;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple MultiTabViews found  \nInstance: " + instance.gameObject.name + " \nThis: " + this.gameObject.name);
            DestroyImmediate(this);
            return;
        }
        instance = this;
        Initialise();
        OpenTab(0);
    }

    private void Start()
    {
    }

    private void Initialise()
    {
        tabs = this.GetComponentsInChildren<TabView>().ToList();

        for (int i = 0; i < TabCount; i++)
        {
            tabs[i].Close();
        }
    }

    public int TabCount
    {
        get
        {
            return tabs.Count;
        }
    }

    public void OpenTab(int index)
    {
        if (index >= 0 & index < TabCount & index != OpenedTab)
        {
            if(OpenedTab >= 0)
                tabs[OpenedTab].Close();
            tabs[index].Open();
            OpenedTab = index;
        }
    }
}
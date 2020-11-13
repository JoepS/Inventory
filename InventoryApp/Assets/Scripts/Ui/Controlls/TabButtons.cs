using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabButtons : MonoBehaviour
{
    private List<TabButton> tabButtons;

    [SerializeField]
    private GameObject tabButtonPrefab = null;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Initialise();
    }

    private void Initialise()
    {
        tabButtons = this.GetComponentsInChildren<TabButton>().ToList();
        if (MultiTabView.instance != null)
        {
            if (MultiTabView.instance.TabCount > tabButtons.Count)
            {
                int difference = MultiTabView.instance.TabCount - tabButtons.Count;
                for (int i = 0; i < difference; i++)
                {
                    tabButtons.Add(CreateButton());
                }
            }
        }
        for (int i = 0; i < tabButtons.Count; i++)
        {
            int index = i;
            tabButtons[i].button.onClick.AddListener(delegate { TabButtonClick(index); });
        }
    }

    private TabButton CreateButton()
    {
        GameObject tabButtonGO = GameObject.Instantiate(tabButtonPrefab, this.transform);
        TabButton tabButton = tabButtonGO.GetComponent<TabButton>();
        return tabButton;
    }

    private void TabButtonClick(int index)
    {
        MultiTabView.instance.OpenTab(index);
    }
}

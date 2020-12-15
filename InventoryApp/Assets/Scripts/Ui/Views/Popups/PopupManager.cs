using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance = null;

    private List<PopupView> popups;

    public PopupView currentPopup = null;

    public bool PopupOpen
    {
        get
        {
            return currentPopup != null;
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple PopupManagers found  \nInstance: " + instance.gameObject.name + " \nThis: " + this.gameObject.name);
            DestroyImmediate(this);
            return;
        }
        instance = this;
        Initialise();
    }

    private void Initialise()
    {
        popups = this.GetComponentsInChildren<PopupView>().ToList();

        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].Close();
        }

        currentPopup = null;
    }

    public void ClosePopup()
    {
        currentPopup.Close();
        currentPopup = null;
    }

    public void OpenPopup(Identifier identifier, params object[] optionals)
    {
        foreach (PopupView popupView in popups)
        {
            if (popupView.Identifier.Equals(identifier)){
                popupView.Open(optionals);
                currentPopup = popupView;
                return;
            }
        }
    }
}

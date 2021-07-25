using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TabButton : MonoBehaviour
{
    public Button button;

    private void Awake()
    {
        button = this.GetComponent<Button>();
    }

}

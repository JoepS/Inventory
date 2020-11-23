using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableValueBool : SelectableValue
{
    private Toggle toggle = null;

    private void Awake()
    {
        toggle = this.GetComponent<Toggle>();
    }
    public override object GetValue()
    {
        return toggle.isOn;
    }

}

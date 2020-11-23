using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableValueText : SelectableValue
{
    private TextField textField;

    private void Awake()
    {
        textField = this.GetComponent<TextField>();
    }
    public override object GetValue()
    {
        return textField.text;
    }
}

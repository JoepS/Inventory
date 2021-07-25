using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableValueText : SelectableValue
{
    private TextField textField;

    private UnityEvent onValueChanged = new UnityEvent();

    private void Awake()
    {
        if(textField == null)
            textField = this.GetComponent<TextField>();
        textField.onValueChanged.AddListener(delegate { onValueChanged.Invoke(); });
    }
    public override object GetValue()
    {
        return textField.text;
    }

    public override void Clear()
    {
        if (textField == null)
            textField = this.GetComponent<TextField>();
        textField.text = "";
    }

    public override void SetValue(object value)
    {
        textField.text = value.ToString();
    }

    public override UnityEvent OnValueChanged()
    {
        return onValueChanged;
    }
}

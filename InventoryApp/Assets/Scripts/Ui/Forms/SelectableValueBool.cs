using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectableValueBool : SelectableValue
{
    [SerializeField]
    private bool DefaultValue = false;
    
    private Toggle toggle = null;

    private UnityEvent onValueChanged = new UnityEvent();

    private void Awake()
    {
        toggle = this.GetComponent<Toggle>();
        this.type = ValueType.Toggle;
        toggle.onValueChanged.AddListener(delegate { onValueChanged.Invoke(); });
    }
    public override object GetValue()
    {
        return toggle.isOn;
    }

    public override void SetValue(object value)
    {
        if (value.GetType().Equals(typeof(bool)))
        {
            toggle.SetIsOnWithoutNotify((bool)value);
        }
    }

    public void SetInteractable(bool value)
    {
        toggle.interactable = value;
    }

    public override void Clear()
    {
        if(toggle != null)
            toggle.isOn = DefaultValue;
    }

    public override UnityEvent OnValueChanged()
    {
        return onValueChanged;
    }
}

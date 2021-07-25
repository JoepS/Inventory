using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SelectableValue : MonoBehaviour
{
    public enum ValueType
    {
        Text,
        Toggle,
        Date,
        Valuta,
        Amount
    }

    public ValueType type = ValueType.Text;

    public Identifier identifier;

    public abstract object GetValue();

    public abstract void SetValue(object value);

    public abstract void Clear();

    public abstract UnityEvent OnValueChanged();
}

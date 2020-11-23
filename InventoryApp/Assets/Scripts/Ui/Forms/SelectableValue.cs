using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

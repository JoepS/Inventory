using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ValueDropdown : TMP_Dropdown
{
    public object GetValue()
    {
        return this.value;
    }
}

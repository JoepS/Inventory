using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableValueDropdown : SelectableValue
{

	private ValueDropdown dropdown;

	private void Awake()
	{
		dropdown = this.GetComponent<ValueDropdown>();
	}

	public override object GetValue()
	{
		return dropdown.GetValue();
	}
}

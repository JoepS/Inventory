using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableValueDropdown : SelectableValue
{

	private ValueDropdown dropdown;

	private UnityEvent onValueChanged = new UnityEvent();

	[SerializeField]
	private DropdownEnumValues enumValues = null;

	private void Awake()
	{
		dropdown = this.GetComponent<ValueDropdown>();
		dropdown.onValueChanged.AddListener(delegate { onValueChanged.Invoke(); });

		if(enumValues != null)
		{
			dropdown.options.Clear();
			foreach(string value in enumValues.enumValues)
			{
				dropdown.options.Add(new TMPro.TMP_Dropdown.OptionData()
				{
					text = value
				});
			}
		}
	}

	public override object GetValue()
	{
		return dropdown.GetValue();
	}

	public override void Clear()
	{
		if(dropdown != null)
			dropdown.value = 0;
	}

	public override void SetValue(object value)
	{
		dropdown.SetValueWithoutNotify((int)value);	
	}

	public override UnityEvent OnValueChanged()
	{
		return onValueChanged;
	}
}

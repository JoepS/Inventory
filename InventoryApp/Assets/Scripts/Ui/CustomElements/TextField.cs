using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextField : TMPro.TMP_InputField
{

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		if (this.m_InputValidator != null)
		{
			if (this.m_InputValidator.GetType().Equals(typeof(TMPDateInputValidator)))
			{
				((TMPDateInputValidator)this.m_InputValidator).Reset();
			}
		}
	}
}

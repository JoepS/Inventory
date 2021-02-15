using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Forms
{

	public class InputFieldBool : FormItemBool
	{
		[SerializeField]
		private TMP_Text labelText = null;

		[SerializeField]
		private Toggle toggle = null;

		public override bool GetValue()
		{
			return toggle.isOn;
		}

		public override void ResetValue()
		{
			toggle.isOn = this.defaultValue;
		}

		public override void Setup(string label, bool defaultValue)
		{
			this.labelText.text = label;
			this.label = label;
			this.defaultValue = defaultValue;
			ResetValue();
		}
	}

}

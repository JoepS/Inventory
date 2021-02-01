using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI.Forms
{

	public class InputFieldText : FormItemString
	{
		[SerializeField]
		private TMP_Text label = null;

		[SerializeField]
		private TMP_InputField inputField = null;

		public override string GetValue()
		{
			return inputField.text;
		}

		public override void ResetValue()
		{
			inputField.text = this.defaultValue;
		}

		public override void Setup(string label, string defaultValue)
		{
			this.label.text = label;
			this.defaultValue = defaultValue;
			ResetValue();
		}
	}

}

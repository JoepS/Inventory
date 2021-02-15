using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI.Forms
{

	public class InputFieldText : FormItemString
	{
		[SerializeField]
		private TMP_Text labelText = null;

		[SerializeField]
		private TMP_InputField inputField = null;

		public override string GetValue()
		{
			return inputField.text;
		}

		public override void ResetValue()
		{
			inputField.placeholder.GetComponent<TMP_Text>().text = this.defaultValue;
		}

		public override void Setup(string label, string defaultValue)
		{
			this.labelText.text = label;
			this.label = label;
			this.defaultValue = defaultValue;
			ResetValue();
		}
	}

}

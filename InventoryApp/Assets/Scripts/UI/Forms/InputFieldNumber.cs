using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI.Forms
{

	public class InputFieldNumber : FormItemFloat
	{
		[SerializeField]
		private TMP_Text labelText = null;

		[SerializeField]
		private TMP_InputField numberField = null;
		public override float GetValue()
		{
			return float.Parse(numberField.text);
		}

		public override void ResetValue()
		{
			numberField.placeholder.GetComponent<TMP_Text>().text = this.defaultValue + "";
		}

		public override void Setup(string label, float defaultValue)
		{
			this.labelText.text = label;
			this.label = label;
			this.defaultValue = defaultValue;
			ResetValue();
		}
	}

}

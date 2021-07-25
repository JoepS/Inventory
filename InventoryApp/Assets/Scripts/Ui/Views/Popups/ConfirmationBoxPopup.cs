using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmationBoxPopup : PopupView
{
	[SerializeField]
	private Button yesButton = null;

	public override void Open(object[] optionals)
	{
		base.Open(optionals);

		ResetYesButton();

		if (optionals.Length > 0)
		{
			if (optionals[0].GetType().Equals(typeof(UnityEvent)))
			{
				yesButton.onClick.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
			}
		}
	}

	private void ResetYesButton()
	{
		yesButton.onClick.RemoveAllListeners();
		yesButton.onClick.AddListener(this.Close);
	}
}

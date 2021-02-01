using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UI.Views
{
    public class ViewLegenda : MonoBehaviour
    {
		List<Button> buttons;

		private void Awake()
		{
			FindButtons();

			foreach(Button b in buttons)
			{
				b.onClick.AddListener(delegate { Debug.Log("Hello " + b.gameObject.GetComponentInParent<TMPro.TMP_Text>().text); });
			}
		}

		private void FindButtons()
		{
			buttons = this.GetComponentsInChildren<Button>().ToList();
		}
	}

}
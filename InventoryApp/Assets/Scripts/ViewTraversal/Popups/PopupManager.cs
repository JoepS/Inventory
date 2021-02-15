using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ViewTraversal.Popup
{
    public class PopupManager : MonoBehaviour
    {
        private static List<Popup> popups = null;

		private static Popup current = null;

		private void Awake()
		{
			popups = this.GetComponentsInChildren<Popup>().ToList();
			CloseAll();
		}

		public static void OpenPopup(Scriptables.Identifier identifier, params object[] parameters)
		{
			if (popups != null) {
				Popup popup = popups.Where(x => x.Identifier.Equals(identifier)).First();
				popup.Open(parameters);
				if (current != null && current != popup)
					current.Close();
				current = popup;
			}
		}

		public static void CloseAll()
		{
			foreach (Popup popup in popups)
				popup.Close();
		}
	}

}

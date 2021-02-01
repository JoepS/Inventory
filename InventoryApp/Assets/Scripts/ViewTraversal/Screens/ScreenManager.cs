using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ViewTraversal.Screens
{
    public class ScreenManager : MonoBehaviour
    {
		private static List<Screen> screens = null;

		private static Screen current = null;

		private void Awake()
		{
			screens = this.GetComponentsInChildren<Screen>().ToList();
		}

		public static void OpenScreen(Scriptables.Identifier identifier, params object[] parameters)
		{
			if (screens != null)
			{
				Screen screen = screens.Where(x => x.Identifier.Equals(identifier)).First();
				screen.Open(parameters);
				if (current != null)
					current.Close();
				current = screen;
			}
		}
	}

}

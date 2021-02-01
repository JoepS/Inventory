using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Views
{

	public abstract class ModelView<T> : MonoBehaviour
	{
		public abstract void View(T model);

		public abstract void UpdateView(T model);

		public void Hide()
		{
			this.gameObject.SetActive(false);
		}

		public void Show()
		{
			this.gameObject.SetActive(true);
		}

		public void Remove()
		{
			DestroyImmediate(this.gameObject);
		}
	}

}

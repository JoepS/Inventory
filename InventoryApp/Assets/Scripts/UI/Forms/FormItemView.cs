using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Forms
{

	public abstract class FormItemView<T> : MonoBehaviour
	{
		public string label;
		public T defaultValue { get; protected set; }
		public abstract void Setup(string label, T defaultValue);
		public abstract T GetValue();
		public abstract void ResetValue();

	}

}

using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace UI.Forms {

    public class ModelForm : ViewTraversal.Popup.Popup
	{
		[SerializeField]
		private Transform content = null;

		[SerializeField]
		private List<FormItemPrefab> prefabs = null;

		public override void Open(params object[] parameters)
		{
			if(parameters != null)
			{
				Form form = null;
				UnityEvent onComplete = null;

				foreach(object o in parameters)
				{
					if (o.GetType().Equals(typeof(Form)))
					{
						form = (Form)o;
					}
					else if (o.GetType().Equals(typeof(UnityEvent)))
					{
						onComplete = (UnityEvent)o;
					}
				}

				if(form != null && onComplete != null)
					CreateForm(form, onComplete);
			}
			else
			{
				Debug.LogError("Expected parameters");
				Close();
			}

			base.Open();
		}

		public void CreateForm(Form form, UnityEvent onComplete)
		{
			foreach(FormItem item in form.items)
			{
				GameObject prefab = prefabs.Where(x => x.type.Equals(item.type)).First().prefab;
				GameObject formItem = Instantiate(prefab, content);
				switch (item.type)
				{
					case FormType.InputFieldText:
						FormItemString formItemString = formItem.GetComponent<FormItemString>();
						formItemString.Setup(item.label, "InputFieldText");
						break;
					case FormType.InputFieldNumber:
						Debug.LogWarning("InputFieldNumber not yet implemented");
						break;
					case FormType.Dropdown:
						Debug.LogWarning("Dropdown not yet implemented");
						break;
					case FormType.Checkbox:
						Debug.LogWarning("Checkbox not yet implemented");
						break;
				}
			}
		}
	}

	[System.Serializable]
	public struct FormItemPrefab
	{
		public FormType type;
		public GameObject prefab;
	}

}

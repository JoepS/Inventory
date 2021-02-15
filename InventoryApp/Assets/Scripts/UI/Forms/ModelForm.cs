using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

namespace UI.Forms {

    public class ModelForm : ViewTraversal.Popup.Popup
	{
		[SerializeField]
		private Transform content = null;

		[SerializeField]
		private FormPrefabs prefabs = null;

		[SerializeField]
		private Button submitButton = null;

		private List<FormItemString> stringItems = new List<FormItemString>();
		private List<FormItemFloat> floatItems = new List<FormItemFloat>();
		private List<FormItemBool> boolItems = new List<FormItemBool>();
		

		private void Awake()
		{
			submitButton.onClick.AddListener(OnSubmitButtonClick);
		}

		public override void Open(params object[] parameters)
		{
			if (parameters != null)
			{
				Form form = null;
				UnityEvent onComplete = null;

				foreach (object o in parameters)
				{
					if (o.GetType().Equals(typeof(Form)))
					{
						form = (Form)o;
					}
					else if (o.GetType().IsAssignableFrom(typeof(UnityEvent)))
					{
						onComplete = (UnityEvent)o;
					}
				}

				if (form != null && onComplete != null)
					CreateForm(form, onComplete);
				else
					Debug.Log("Form: " + form + " onComplete: " + onComplete);
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
			for (int i = 0; i < content.childCount; i++)
			{
				Destroy(content.GetChild(i).gameObject);
			}

			foreach(FormItem item in form.items)
			{
				GameObject prefab = prefabs.items.Where(x => x.type.Equals(item.type)).First().prefab;
				GameObject formItem = Instantiate(prefab, content);
				switch (item.type)
				{
					case FormType.InputFieldText:
						FormItemString formItemString = formItem.GetComponent<FormItemString>();
						formItemString.Setup(item.label, "InputFieldText");
						stringItems.Add(formItemString);
						break;
					case FormType.InputFieldNumber:
						FormItemFloat formItemFloat = formItem.GetComponent<FormItemFloat>();
						formItemFloat.Setup(item.label, 0);
						floatItems.Add(formItemFloat);
						break;
					case FormType.Dropdown:
						Debug.LogWarning("Dropdown not yet implemented");
						break;
					case FormType.Checkbox:
						FormItemBool formItemBool = formItem.GetComponent<FormItemBool>();
						formItemBool.Setup(item.label, false);
						boolItems.Add(formItemBool);
						break;
				}
			}

		}

		private void OnSubmitButtonClick()
		{
			List<LabelItem> info = new List<LabelItem>();
			foreach (FormItemString fis in stringItems)
				info.Add(new LabelItem() { label = fis.label, item = fis.GetValue() });
			foreach (FormItemFloat fif in floatItems)
				info.Add(new LabelItem() { label = fif.label, item = fif.GetValue() });
			foreach (FormItemBool fib in boolItems)
				info.Add(new LabelItem() { label = fib.label, item = fib.GetValue() });


		}
	}

	public class LabelItem
	{
		public string label;
		public object item;
	}

}

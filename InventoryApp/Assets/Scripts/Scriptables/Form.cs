using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{

    [CreateAssetMenu(fileName ="new form", menuName = "Form/New Form", order = 0)]
    public class Form : ScriptableObject
    {
        public string title;
        public List<FormItem> items;
    }

    [System.Serializable]
    public class FormItem
	{
        public string label;
        public FormType type;
	}

    public enum FormType
	{
        InputFieldText,
        InputFieldNumber,
        Dropdown,
        Checkbox,

	}
}

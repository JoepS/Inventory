using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{

    [CreateAssetMenu(fileName = "new form prefabs", menuName = "Form/Prefabs", order = 1)]
    public class FormPrefabs : ScriptableObject
    {
        public List<FormItemPrefab> items;
    }

    [System.Serializable]
    public struct FormItemPrefab
    {
        public FormType type;
        public GameObject prefab;
    }

}
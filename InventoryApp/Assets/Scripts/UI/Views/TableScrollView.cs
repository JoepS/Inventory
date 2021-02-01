using DataManagement.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

namespace UI.Views
{
    [RequireComponent(typeof(ScrollView))]
    public class TableScrollView : MonoBehaviour
    {
        [SerializeField]
        private GameObject modelViewPrefab = null;

        [SerializeField]
        private Transform content = null;

        public void ViewTable<T>(List<T> tableList) where T  : Model
		{
            foreach(T model in tableList)
			{
                CreateModelView(model);
			}
		}

        private void CreateModelView<T>(T model) where T : Model
		{
            GameObject view = GameObject.Instantiate(modelViewPrefab, content);
            ModelView<T> modelView = view.GetComponent<ModelView<T>>();
            if(modelView == null)
			{
                Debug.LogError("No correct type of ModelView found! (" + typeof(T) + ")");
                DestroyImmediate(view);
                return;
			}
            modelView.View(model);
            modelView.Show();
		}
    }

}

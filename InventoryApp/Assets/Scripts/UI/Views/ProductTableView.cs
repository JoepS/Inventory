using DataManagement.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ViewTraversal.Popup;

namespace UI.Views
{

    public class ProductTableView : ViewTraversal.Screens.Screen
    {
        [SerializeField]
        private TableScrollView tableScrollView = null;

        [SerializeField]
        private Button addButton = null;

        [SerializeField]
        private Scriptables.Identifier modelFormPopup = null;

        [SerializeField]
        private Scriptables.Form addForm = null;

        private void Awake()
        {
            addButton.onClick.AddListener(OnAddButtonClick);
        }

        void Start()
        {
            tableScrollView.ViewTable(DataManagement.DatabaseController.connection.Table<Product>().ToList());
        }

        private void OnAddButtonClick()
        {
            InfoEvent completeEvent = new InfoEvent();
            completeEvent.AddListener(NewProduct);
            PopupManager.OpenPopup(modelFormPopup, addForm, completeEvent);
        }

        private void NewProduct(List<Forms.LabelItem> info)
		{
            foreach(Forms.LabelItem o in info)
			{
                Debug.Log(o.label + ": " + o.item);
			}
		}


    }

    public class InfoEvent : UnityEvent<List<Forms.LabelItem>>
    {

    }

}
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
            UnityEvent completeEvent = new UnityEvent();
            completeEvent.AddListener(delegate { Debug.Log("On Complete Form"); });
            PopupManager.OpenPopup(modelFormPopup, addForm,  completeEvent);
		}


    }

}

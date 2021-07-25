using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagement;

public class ShoppingListView : TabView
{
	[SerializeField]
	private GameObject shoppingListItemPrefab = null;

	[SerializeField]
	private Transform listContent = null;

	ShoppingList shoppingList = null;


	private void Start()
	{
		shoppingList = DataManager.instance.GetJsonObject<ShoppingList>();
		UpdateShoppingList();
	}

	private void UpdateShoppingList()
	{
		List<ProductAmount> list = shoppingList.list;
		foreach(ProductAmount amount in list)
		{
			ShoppingListItem item = GameObject.Instantiate(shoppingListItemPrefab, listContent).GetComponent<ShoppingListItem>();
			item.Setup(amount);
		}
	}
}

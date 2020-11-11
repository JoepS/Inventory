using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

public class ProductListView : MonoBehaviour
{
    [SerializeField]
    private Button addButton = null;

    [SerializeField]
    private Transform productsContent = null;

    [SerializeField]
    private GameObject productViewPrefab = null;

    List<Product> products = null;

    private void Start()
    {
        addButton.onClick.AddListener(AddButtonClick);
        products = DataManager.instance.GetProductList().products;
        CreateProductList();
    }

    private void AddButtonClick()
    {

    }

    private void CreateProductList()
    {
        foreach(Product product in products)
        {
            GameObject productViewGameObject = GameObject.Instantiate(productViewPrefab, productsContent);
            productViewGameObject.name = product.name;

            ProductView productView = productViewGameObject.GetComponent<ProductView>();
            productView.SetProduct(product);
        }
    }
}

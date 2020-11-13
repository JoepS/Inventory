using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;

public class ProductListView : TabView
{
    [SerializeField]
    private Button addButton = null;

    [SerializeField]
    private Transform productsContent = null;

    [SerializeField]
    private GameObject productViewPrefab = null;

    List<Product> products = null;

    List<ProductView> productViews = null;

    private void Start()
    {
        addButton.onClick.AddListener(AddButtonClick);
        products = DataManager.instance.GetProductList().products;
        productViews = new List<ProductView>();
        CreateProductList();
    }

    private void AddButtonClick()
    {

    }

    private ProductView CreateProductView(Product product)
    {
        GameObject productViewGameObject = GameObject.Instantiate(productViewPrefab, productsContent);
        productViewGameObject.name = product.name;

        ProductView productView = productViewGameObject.GetComponent<ProductView>();
        productView.SetProduct(product);

        return productView;
    }

    private void CreateProductList()
    {
        foreach(Product product in products)
        {
            ProductView productView = CreateProductView(product);
            productViews.Add(productView);
        }
    }

    private void UpdateProuctList()
    {
        for (int i = 0; i < products.Count; i++)
        {
            ProductView productView = null;
            if (i >= productViews.Count)
            {
                productView = CreateProductView(products[i]);
            }
            else
            {
                productView = productViews[i];
                productView.UpdateProduct(products[i]);
            }
        }
    }
}

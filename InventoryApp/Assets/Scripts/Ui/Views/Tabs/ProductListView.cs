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

    [SerializeField]
    private Identifier addProductIdentifier = null;

    List<Product> products = null;

    List<ProductView> productViews = new List<ProductView>();

    private void Start()
    {
        addButton.onClick.AddListener(AddButtonClick);
        products = DataManager.instance.GetProductList().products;
        UpdateProductList();
    }

    public override void Open()
    {
        if (DataManager.instance.Ready)
        {
            products = DataManager.instance.GetProductList().products;
            UpdateProductList();
        }
        base.Open();
    }

    private void AddButtonClick()
    {
        PopupManager.instance.OpenPopup(addProductIdentifier);
    }

    private ProductView CreateProductView(Product product)
    {
        GameObject productViewGameObject = GameObject.Instantiate(productViewPrefab, productsContent);
        productViewGameObject.name = product.name;

        ProductView productView = productViewGameObject.GetComponent<ProductView>();
        productView.SetProduct(product);

        productViews.Add(productView);

        return productView;
    }

    private void UpdateProductList()
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

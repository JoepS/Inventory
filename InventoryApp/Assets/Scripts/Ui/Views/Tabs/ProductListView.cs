using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagement;
using UnityEngine.Events;
using System.Linq;

public class ProductListView : TabView
{
    [SerializeField]
    private Transform productsContent = null;

    [SerializeField]
    private GameObject productViewPrefab = null;


    List<Product> products = null;

    List<ProductView> productViews = new List<ProductView>();

    private void Start()
    {
        products = DataManager.instance.GetList<Product>().list;
        UpdateProductList();

    }

    public override void Open()
    {
        if (DataManager.instance.Ready)
        {
            products = DataManager.instance.GetList<Product>().list;
            UpdateProductList();
        }
        base.Open();
    } 

    private ProductView CreateProductView(Product product)
    {
        GameObject productViewGameObject = GameObject.Instantiate(productViewPrefab, productsContent);
        productViewGameObject.name = product.name+"View";

        ProductView productView = productViewGameObject.GetComponent<ProductView>();
        productView.SetProduct(product);
        productView.SetOptionsMenu(optionsMenu);
        productViews.Add(productView);
        productView.removeToggleSwitch.AddListener(RemoveToggleSwitch);
        productView.OnClick.AddListener(OnProductViewClick);

        return productView;
    }

    private void UpdateProductList()
    {
        foreach (ProductView pv in productViews)
            pv.Hide();
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
            productView.Show();
        }
    }

    protected override void SetNoneState()
    {
        base.SetNoneState();
        SetRemoveState(false);
    }

    protected override void SetAddState()
    {
        base.SetAddState();
        UnityEvent addCompletedEvent = new UnityEvent();
        addCompletedEvent.AddListener(CompletedAdding);
        PopupManager.instance.OpenPopup(addIdentifier, addCompletedEvent);
    }

    protected override void SetRemoveState(bool value)
    {
        base.SetRemoveState(value);
        foreach (ProductView pv in productViews)
        {
            if(pv.gameObject.activeSelf)
                pv.SetRemoveState(value);
        }
    }

    private void OnProductViewClick(Product product)
    {
        if (optionsMenu.currentState.Equals(OptionsMenu.OptionsState.Edit))
        {
            UnityEvent editEndEvent = new UnityEvent();
            editEndEvent.AddListener(EndEditing);
            PopupManager.instance.OpenPopup(editIdentifier, editEndEvent, product);
        }
    }

    protected override void RemoveButtonClick()
    {
        UnityEvent removeConfirmation = new UnityEvent();
        removeConfirmation.AddListener(RemoveConfirmation);
        PopupManager.instance.OpenPopup(confirmationIdentifier, removeConfirmation);
    }

    private void RemoveConfirmation()
    {
        foreach (ProductView pv in productViews)
        {
            if (pv.GetToggleState)
            {
                DataManager.instance.GetList<Product>().list.Remove(pv.GetProduct());
                JsonObjectList<ProductAmount> paList = DataManager.instance.GetList<ProductAmount>();
                paList.list.Remove(paList.list.Where(x => x.ProductId.Equals(pv.GetProduct().id)).First());
            }
        }

        UpdateProductList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }

    private void CompletedAdding()
    {
        UpdateProductList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }

    private void EndEditing()
    {
        UpdateProductList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }

    private void RemoveToggleSwitch(bool toggled)
    {
        if (!toggled)
        {
            foreach (ProductView pv in productViews)
            {
                if (pv.GetToggleState)
                    return;
            }
            SetRemoveState(false);
        }
    }
}

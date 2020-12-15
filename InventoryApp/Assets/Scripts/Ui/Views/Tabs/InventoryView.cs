using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryView : TabView
{
    [SerializeField]
    private Transform content = null;

    [SerializeField]
    private GameObject amountViewPrefab = null;

    private List<Product> products = new List<Product>();

    private List<ProductAmount> amounts = new List<ProductAmount>();

    private List<ProductAmountView> amountViews = new List<ProductAmountView>();

    public override void Open()
    {
        if (DataManager.instance.Ready)
        {
            products = DataManager.instance.GetList<Product>().list;
            amounts = DataManager.instance.GetList<ProductAmount>().list;
            UpdateProductAmountList();
        }
        base.Open();
    }

    private ProductAmountView CreateProductAmountView(ProductAmount amount)
    {
        GameObject productAmountViewGameObject = GameObject.Instantiate(amountViewPrefab, content);
        Product product = products.Where(x => x.id == amount.ProductId).First();
        productAmountViewGameObject.name = product.name + "AmountView";

        ProductAmountView productAmountView = productAmountViewGameObject.GetComponent<ProductAmountView>();
        productAmountView.Setup(product, amount);
        productAmountView.SetOptionsMenu(optionsMenu);
        amountViews.Add(productAmountView);
        productAmountView.removeToggleSwitch.AddListener(RemoveToggleSwitch);
        productAmountView.OnClick.AddListener(OnProductAmountViewClick);

        return productAmountView;
    }

    private void UpdateProductAmountList()
    {
        foreach (ProductAmountView pv in amountViews)
            pv.Hide();
        for (int i = 0; i < amounts.Count; i++)
        {
            ProductAmountView amountView = null;
            if (i >= amountViews.Count)
            {
                amountView = CreateProductAmountView(amounts[i]);
            }
            else
            {
                amountView = amountViews[i];
                amountView.Setup(products.Where(x => x.id == amounts[i].ProductId).First(), amounts[i]);
            }
            amountView.Show();
        }
    }

    private void RemoveToggleSwitch(bool toggled)
    {
        if (!toggled)
        {
            foreach (ProductAmountView pv in amountViews)
            {
                if (pv.GetToggleState)
                    return;
            }
            SetRemoveState(false);
        }
    }

    private void OnProductAmountViewClick(ProductAmount productAmount)
    {
        if (optionsMenu.currentState.Equals(OptionsMenu.OptionsState.Edit))
        {
            UnityEvent editEndEvent = new UnityEvent();
            editEndEvent.AddListener(EndEditing);
            PopupManager.instance.OpenPopup(editIdentifier, editEndEvent, productAmount);
        }
    }

    protected override void SetAddState()
    {
        base.SetAddState();
        UnityEvent addCompletedEvent = new UnityEvent();
        addCompletedEvent.AddListener(CompletedAdding);
        PopupManager.instance.OpenPopup(addIdentifier, addCompletedEvent);
    }
    private void CompletedAdding()
    {
        UpdateProductAmountList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }

    private void EndEditing()
    {
        UpdateProductAmountList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }

    protected override void RemoveButtonClick()
    {
        UnityEvent removeConfirmation = new UnityEvent();
        removeConfirmation.AddListener(RemoveConfirmation);
        PopupManager.instance.OpenPopup(confirmationIdentifier, removeConfirmation);
    }

    protected override void SetRemoveState(bool value)
    {
        base.SetRemoveState(value);
        foreach (ProductAmountView pv in amountViews)
        {
            if (pv.gameObject.activeSelf)
                pv.SetRemoveState(value);
        }
    }

    protected override void SetNoneState()
    {
        base.SetNoneState();
        SetRemoveState(false);
    }

    private void RemoveConfirmation()
    {
        foreach (ProductAmountView pv in amountViews)
        {
            if (pv.GetToggleState)
            {
                DataManager.instance.GetList<ProductAmount>().list.Remove(pv.GetProductAmount());
            }
        }

        UpdateProductAmountList();
        this.optionsMenu.SetState(OptionsMenu.OptionsState.None);
    }
}

using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ingredientName = null;

    [SerializeField]
    private TMP_Text amount = null;

    [SerializeField]
    private Button removeButton = null;

    private PopupView parent;

    public ProductAmount productAmount {
        get;
        private set;
    }

    private void Awake()
    {
        removeButton.onClick.AddListener(RemoveButtonClick);
    }

    public bool IsHidden()
    {
        return !this.gameObject.activeSelf;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void SetRemovable(bool value)
    {
        removeButton.gameObject.SetActive(value);
    }

    public void View(ProductAmount productAmount)
    {
        this.productAmount = productAmount;
        ingredientName.text = DataManager.instance.GetList<Product>().list.Where(x => x.id == productAmount.ProductId).First().name;
        amount.text = productAmount.amount.value + " " + productAmount.amount.type.ToString();
        Show();
    }

    public void View(ProductAmount productAmount, PopupView parent)
    {
        this.parent = parent;
        View(productAmount);
    }

    public void RemoveButtonClick()
    {
        if (parent != null)
        {
            parent.Remove(productAmount);
        }
    }
}

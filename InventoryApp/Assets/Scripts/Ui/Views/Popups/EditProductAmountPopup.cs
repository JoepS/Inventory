using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditProductAmountPopup : PopupView
{
    [SerializeField]
    private TMPro.TMP_Text productText = null;

    [SerializeField]
    private SelectableValueBool toggle = null;

    [SerializeField]
    private GameObject perishDatePanel = null;

    [SerializeField]
    private Button submitButton = null;

    [SerializeField]
    private Identifier perishDate = null;

    [SerializeField]
    private Identifier amountValue = null;

    [SerializeField]
    private Identifier amountType = null;


    private ProductAmount currentEdit = null;

    protected override void Awake()
    {
        base.Awake();
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    public override void Open(object[] optionals)
    {
        base.Open(optionals);

        toggle.SetInteractable(false);

        if (optionals.Length > 1)
        {
            if (optionals[0].GetType().Equals(typeof(UnityEvent)))
            {
                Debug.Log("Adding optionals event ");
                onClose.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
            }
            if (optionals[1].GetType().Equals(typeof(ProductAmount)))
            {
                currentEdit = (ProductAmount)optionals[1];
                Setup(currentEdit);
            }
        }
        else
        {
            Close();
        }
    }

    private void Setup(ProductAmount productAmount)
    {
        Product product = DataManager.instance.GetList<Product>().list.Where(x => x.id == productAmount.ProductId).First();
        productText.text = product.name;

        toggle.SetValue(product.perishable);
        perishDatePanel.SetActive(product.perishable);

        GetFromIdentifier(perishDate).SetValue(productAmount.perishDate);
        GetFromIdentifier(amountValue).SetValue(productAmount.amount.value);
        GetFromIdentifier(amountType).SetValue(productAmount.amount.type);
    }

    private void OnSubmitButtonClicked()
    {
        currentEdit.perishDate = GetFromIdentifier(perishDate).GetValue().ToString() ;


        float amount = 0;
        float.TryParse(GetFromIdentifier(amountValue).GetValue().ToString(), out amount);
        currentEdit.amount.value = amount;

        currentEdit.amount.type = (Amount.Type)GetFromIdentifier(amountType).GetValue();

        List<ProductAmount> sameProducts = DataManager.instance.GetList<ProductAmount>().list.Where(x => x.ProductId == currentEdit.ProductId && x.id != currentEdit.id).ToList();

        bool edited = false;

        foreach(ProductAmount productAmount in sameProducts)
        {
            if (productAmount.amount.type.Equals(currentEdit.amount.type) && productAmount.perishDate.Equals(currentEdit.perishDate))
            {
                productAmount.amount.value += amount;
                DataManager.instance.RemoveItem(currentEdit);
                edited = true;
            }
        }

        if (!edited)
        {
            DataManager.instance.EditItem(currentEdit);
        }
        this.Close();
    }


}

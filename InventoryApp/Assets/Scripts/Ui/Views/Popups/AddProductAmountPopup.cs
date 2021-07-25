using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddProductAmountPopup : PopupView
{
    [SerializeField]
    private ValueDropdown productDropdown = null;

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


    private Product selectedProduct = null;


    protected override void Awake()
    {
        base.Awake();
        productDropdown.onValueChanged.AddListener(DropDownValueChanged);
        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    public override void Open(object[] optionals)
    {
        base.Open(optionals); 
        
        if (optionals.Length > 0)
        {
            if (optionals[0].GetType().Equals(typeof(UnityEvent)))
            {
                onClose.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
            }
        }

        Setup();
    }


    private void Setup()
    {
        List<Product> products = DataManager.instance.GetList<Product>().list;
        List<ValueDropdown.OptionData> options = new List<ValueDropdown.OptionData>();
        foreach(Product product in products)
        {
            options.Add(new ValueDropdown.OptionData()
            {
                text = product.name
            });
        }
        productDropdown.options = options;
        toggle.SetInteractable(false);
        productDropdown.SetValueWithoutNotify(0);
        DropDownValueChanged(0);
    }

    private void DropDownValueChanged(int value)
    {
        Debug.Log("OnDropDownValueChanged " + value);
        selectedProduct = DataManager.instance.GetList<Product>().list[value];
        toggle.SetValue(selectedProduct.perishable);

        perishDatePanel.SetActive(selectedProduct.perishable);
    }

    private void OnSubmitButtonClick()
    {
        string perishDate = "";
        if ((bool)toggle.GetValue())
        {

            perishDate = (string)GetFromIdentifier(this.perishDate).GetValue();

        }

        float amount = 0;
        float.TryParse(GetFromIdentifier(amountValue).GetValue().ToString(), out amount);


        Amount.Type type = (Amount.Type)GetFromIdentifier(amountType).GetValue();

        List<ProductAmount> sameProducts = DataManager.instance.GetList<ProductAmount>().list.Where(x => x.ProductId == selectedProduct.id).ToList();

        bool added = false;

        foreach(ProductAmount productAmount in sameProducts)
        {
            if (productAmount.amount.type.Equals(type) && productAmount.perishDate.Equals(perishDate))
            {
                productAmount.amount.value += amount;
                added = true;
            }
        }
        if (!added)
        {
            DataManager.instance.AddItem<ProductAmount>(new ProductAmount()
            {
                perishDate = perishDate,
                amount = new Amount()
                {
                    type = type,
                    value = amount
                },
                ProductId = selectedProduct.id

            });
        }

        Close();
    }
}

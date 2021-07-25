using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddProductPopup : PopupView
{
    [SerializeField]
    private Button submitButton = null;


    [SerializeField]
    private GameObject perishDatePanel = null;

    [SerializeField]
    private Identifier productName = null;
    [SerializeField]
    private Identifier productDescription = null;
    [SerializeField]
    private Identifier productPerishable = null;
    [SerializeField]
    private Identifier productCost = null;
    [SerializeField]
    private Identifier productAmountValue = null;
    [SerializeField]
    private Identifier productAmountType = null;
    [SerializeField]
    private Identifier productPerishDate = null;




    protected override void Awake()
    {
        base.Awake();


        submitButton.onClick.AddListener(OnSubmitButtonClick);

        GetFromIdentifier(productPerishable).OnValueChanged().AddListener(OnPerishableValueChanged);
    }
    
    public override void Open(object[] optionals)
    {
        base.Open(optionals);

        if(optionals.Length > 0)
        {
            if (optionals[0].GetType().Equals(typeof(UnityEvent)))
            {
                onClose.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
            }
        }
        this.perishDatePanel.SetActive((bool)GetFromIdentifier(productPerishable).GetValue());

    }

    private void OnSubmitButtonClick()
    {
        string name = (string)GetFromIdentifier(productName).GetValue();
        string description = (string)GetFromIdentifier(productDescription).GetValue();
        bool perishable = (bool)GetFromIdentifier(productPerishable).GetValue();

        float cost = 0;
        float.TryParse(GetFromIdentifier(productCost).GetValue().ToString(), out cost);



        float amount = 0;
        float.TryParse(GetFromIdentifier(productAmountValue).GetValue().ToString(), out amount);

        Amount.Type type = (Amount.Type)GetFromIdentifier(productAmountType).GetValue();

        string perishDate = "";
        if (perishable)
        {

            perishDate = (string)GetFromIdentifier(productPerishDate).GetValue();

        }

        

        Product product = DataManager.instance.AddItem(new Product()
        {
            name = name,
            description = description,
            perishable = perishable,
            cost = cost            
        });

        ProductAmount productAmount = DataManager.instance.AddItem(new ProductAmount()
        {
            amount = new Amount()
            {
                value = amount,
                type = type
            },
            perishDate = perishDate,
            ProductId = product.id
        });

        Debug.Log(string.Format("New Product: {0}, {1}, {2}, {3}, {4}, {5}, {6}", name, description, perishable, cost, amount, type, perishDate));
        Debug.Log(string.Format("Amount: {0}", productAmount));
        Close();
    }

    

    public override void Close()
    {
        base.Close();
        ClearInputs();
    }

    private void ClearInputs()
    {
        foreach(SelectableValue sv in selectables)
        {
            sv.Clear();
        }
    }

    private void OnPerishableValueChanged()
    {
        this.perishDatePanel.SetActive((bool)GetFromIdentifier(productPerishable).GetValue());
    }
}
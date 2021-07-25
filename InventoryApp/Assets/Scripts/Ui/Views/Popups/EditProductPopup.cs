using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditProductPopup : PopupView
{
    [SerializeField]
    private Button submitButton = null;

    [SerializeField]
    private Identifier productName = null;
    [SerializeField]
    private Identifier productDescription = null;
    [SerializeField]
    private Identifier productPerishable = null;
    [SerializeField]
    private Identifier productCost = null;

    private Product currentEdit;

    protected override void Awake()
    {
        base.Awake();

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }


    public override void Open(object[] optionals)
    {
        base.Open(optionals);

        if (optionals.Length > 1)
        {
            if (optionals[0].GetType().Equals(typeof(UnityEvent)))
            {
                Debug.Log("Adding optionals event ");
                onClose.AddListener(delegate { ((UnityEvent)optionals[0]).Invoke(); });
            }
            if (optionals[1].GetType().Equals(typeof(Product)))
            {
                currentEdit = (Product)optionals[1];
                SetupProductEdit(currentEdit);
            }
        }
        else
        {
            Close();
        }

    }

    private void SetupProductEdit(Product product)
    {
        GetFromIdentifier(productName).SetValue(product.name);
        GetFromIdentifier(productDescription).SetValue(product.description);
        GetFromIdentifier(productPerishable).SetValue(product.perishable);
        GetFromIdentifier(productCost).SetValue(product.cost);
        
    }

    private void OnSubmitButtonClick()
    {
        Debug.Log("Submit");

        currentEdit.name = (string)GetFromIdentifier(productName).GetValue();
        currentEdit.description = (string)GetFromIdentifier(productDescription).GetValue();
        currentEdit.perishable = (bool)GetFromIdentifier(productPerishable).GetValue();

        float cost = 0;
        float.TryParse(GetFromIdentifier(productCost).GetValue().ToString(), out cost);
        currentEdit.cost = cost;

        

        DataManager.instance.EditItem(currentEdit);


        Debug.Log(string.Format("Edit Product: {0}", currentEdit));

        Close();
    }

    public override void Close()
    {
        base.Close();
    }

    

}
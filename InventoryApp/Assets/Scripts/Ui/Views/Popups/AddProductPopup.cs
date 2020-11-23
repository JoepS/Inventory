using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddProductPopup : PopupView
{
    [SerializeField]
    private Button submitButton = null;

    [SerializeField]
    private GameObject formContent = null;

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

    private List<SelectableValue> selectables;



    protected override void Awake()
    {
        base.Awake();

        selectables = formContent.GetComponentsInChildren<SelectableValue>().ToList();

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    private void OnSubmitButtonClick()
    {
        Debug.Log("Submit");

        string name = (string)GetFromIdentifier(productName).GetValue();
        string description = (string)GetFromIdentifier(productDescription).GetValue();
        bool perishable = (bool)GetFromIdentifier(productPerishable).GetValue();

        float cost = 0;
        float.TryParse(GetFromIdentifier(productCost).GetValue().ToString(), out cost);

        float amount = 0;
        float.TryParse(GetFromIdentifier(productAmountValue).GetValue().ToString(), out amount);

        string type = (string)GetFromIdentifier(productAmountType).GetValue();

        string perishDate = (string)GetFromIdentifier(productPerishDate).GetValue();

        DataManagement.DataManager.instance.AddProduct(new DataManagement.Product()
        {
            name = name,
            description = description,
            perishable = perishable,
            cost = cost            
        });

        Debug.Log(string.Format("New Product: {0}, {1}, {2}, {3}, {4}, {5}, {6}", name, description, perishable, cost, amount, type, perishDate));
    }

    private SelectableValue GetFromIdentifier(Identifier identifier)
    {
        foreach(SelectableValue selectableValue in selectables)
        {
            if (selectableValue.identifier.Equals(identifier))
            {
                return selectableValue;
            }
        }
        return null;
    }
}
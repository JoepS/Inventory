using DataManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShoppingListItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameLabel = null;

    [SerializeField]
    private TMP_Text amountValue = null;

    [SerializeField]
    private TMP_Text amountType = null;


    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Setup(ProductAmount amount)
    {
        nameLabel.text = DataManager.instance.GetList<Product>().list.Where(x => x.id.Equals(amount.ProductId)).First().name;
        amountValue.text = "" + amount.amount.value;
        amountType.text = "" + amount.amount.type.ToString();
    }
}
